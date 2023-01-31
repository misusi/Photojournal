using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Photoblog.Data;
using Photoblog.Data.Repository.IRepository;
using Photoblog.Models;
using Photoblog.Models.ViewModels;

namespace Photoblog.Web.Areas.Admin.Controllers
{
    public class PhotoblogEntryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PhotoblogEntryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Create(PhotoblogEntryViewModel photoblogEntryViewModel, List<IFormFile> postedFiles)
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
                PhotoblogEntry newPhotoblogEntry = new();
                newPhotoblogEntry.Title = photoblogEntryViewModel.PhotoSetTitle;
                newPhotoblogEntry.Description = photoblogEntryViewModel.PhotoSetDescription;
                newPhotoblogEntry.DateCreated = DateTime.Now;
                // create new photo object for each image url included
                foreach (var file in postedFiles)
                {
                    Photo newPhotoToAdd = new Photo
                    {
                        ImageUrl = file.FileName,
                        Description = "", // TODO: Have one desc. for set or also per photo?
                        DateTaken = photoblogEntryViewModel.PhotoSetDate,
                        LocationTakenLat = photoblogEntryViewModel.PhotoLocationLat,
                        LocationTakenLng = photoblogEntryViewModel.PhotoLocationLng,
                        DateUploaded = newPhotoblogEntry.DateCreated
                    };
                    newPhotoblogEntry.PhotoList.Add(newPhotoToAdd);

                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);


                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    // convert image if cr2 + change path
                    if (extension.ToLower() == ".cr2")
                    {
                        CR2ToJPG.Converter.ConvertImage(Path.Combine(uploads, fileName + extension), Path.Combine(uploads));
                        newPhotoblogEntry.PhotoList.Last().ImageUrl = @"\images\photos\" + fileName + ".jpg";
                    }
                    else
                    {
                        newPhotoblogEntry.PhotoList.Last().ImageUrl = @"\images\photos\" + fileName + extension;
                    }

                }
                
                // add to db
                _unitOfWork.PhotoblogEntry.Add(newPhotoblogEntry);
                _unitOfWork.Save();
                return RedirectToAction("Index", "PhotoblogEntry");
            }
            return View(photoblogEntryViewModel);
        }

        #region API_CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var photoblogEntries = _unitOfWork.PhotoblogEntry.GetAll(includeProperties: "PhotoList");
            return Json(new { data = photoblogEntries });
        }

    }
    #endregion
}
