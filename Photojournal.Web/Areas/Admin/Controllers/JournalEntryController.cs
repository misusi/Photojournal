using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Photojournal.Data;
using Photojournal.Data.Repository.IRepository;
using Photojournal.Models;
using Photojournal.Models.ViewModels;
using Photojournal.Utils.CR2Converter;

namespace Photojournal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JournalEntryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public JournalEntryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            //ModelState.Clear();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(100000000000)]
        public IActionResult Create(JournalEntryViewModel journalEntryViewModel, List<IFormFile> postedFiles)
        {
            //if (postedFiles.Count < 1)
            //{
            //    ModelState.AddModelError("postedFiles", "Please select at least one photo to upload.");
            //}
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var uploads = Path.Combine(wwwRootPath, @"images\photos");
                // create new photoblog entry from data in view model
                JournalEntry newJournalEntry = new();
                newJournalEntry.Title = journalEntryViewModel.JournalEntry.Title;
                newJournalEntry.Description = journalEntryViewModel.JournalEntry.Description;
                newJournalEntry.DateCreated = DateTime.Now;
                // create new photo object for each image url included
                foreach (var file in postedFiles)
                {
                    Photo newPhotoToAdd = new Photo
                    {
                        ImageUrl = file.FileName,
                        Description = "", // TODO: Have one desc. for set or also per photo?
                        DateTaken = journalEntryViewModel.PhotoSetDate,
                        LocationTakenLat = journalEntryViewModel.PhotoLocationLat,
                        LocationTakenLng = journalEntryViewModel.PhotoLocationLng,
                        DateUploaded = newJournalEntry.DateCreated
                    };
                    newJournalEntry.PhotoList.Add(newPhotoToAdd);

                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);


                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    // convert image if cr2 + change path
                    if (extension.ToLower() == ".cr2")
                    {
                        Converter.ConvertImage(Path.Combine(uploads, fileName + extension), Path.Combine(uploads));
                        // remove old file
                        System.IO.File.Delete(Path.Combine(uploads, fileName + extension));
                        newJournalEntry.PhotoList.Last().ImageUrl = @"\images\photos\" + fileName + ".jpg";
                    }
                    else
                    {
                        newJournalEntry.PhotoList.Last().ImageUrl = @"\images\photos\" + fileName + extension;
                    }

                }
                
                // add to db
                _unitOfWork.JournalEntry.Add(newJournalEntry);
                _unitOfWork.Save();
                //return RedirectToAction("Index", "JournalEntry");
                // needed for redirecting after ajax post
                return Json(new { redirectToUrl = Url.Action("Index", "JournalEntry") });
            }
            return View(journalEntryViewModel);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var journalEntryFromDb = _unitOfWork.JournalEntry.GetFirstOrDefault(u => u.Id == id, 
                includeProperties:"PhotoList");
            if (journalEntryFromDb == null)
            {
                return NotFound();
            }

            // make view model from retrieved category
            JournalEntryViewModel jevm = new();
            jevm.JournalEntry = journalEntryFromDb;
            jevm.PhotoLocationLng = journalEntryFromDb.PhotoList.Last().LocationTakenLng;
            jevm.PhotoLocationLat = journalEntryFromDb.PhotoList.Last().LocationTakenLat;
            jevm.PhotoSetDate = journalEntryFromDb.PhotoList.Last().DateTaken;

            return View(jevm);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JournalEntryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.JournalEntry.Update(obj.JournalEntry);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index", "JournalEntry");
            }
            return View(obj);
        }

        #region API_CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var journalEntries = _unitOfWork.JournalEntry.GetAll(includeProperties: "PhotoList");
            return Json(new { data = journalEntries });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.JournalEntry.GetFirstOrDefault(u => u.Id == id, includeProperties:"PhotoList");
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            // Remove images from file system
            foreach (var photo in obj.PhotoList)
            {
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                    photo.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.JournalEntry.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    } //end class
}//end namespace