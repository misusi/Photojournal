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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoblogEntryViewModel photoblogEntryViewModel, List<IFormFile> postedFiles)
        {
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

                //// also copy image to project folder
                //string destImageDirPathLocal = @"\images\reviews";
                //string destImageDirPathFull = Path.Combine(_webEnvironment.WebRootPath, destImageDirPathLocal);
                //if (!System.IO.Directory.Exists(destImageDirPathFull))
                //{
                //    System.IO.Directory.CreateDirectory(destImageDirPathFull);
                //}

                //string imageExt = Path.GetExtension(newPhotoblogEntry.PhotoList.Last().ImageUrl);
                //string destImageFileName = Guid.NewGuid().ToString() + imageExt;
                //string srcImagePathFull = Path.GetFullPath(newPhotoblogEntry.PhotoList.Last().ImageUrl);
                //string finalImagePath = Path.Combine(destImageDirPathFull, destImageFileName);
                //for (int i = 0; i < postedFiles.Count; i++)
                //{
                //    System.IO.File.Copy(postedFiles[i].FileName, finalImagePath);

                //}

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var uploads = Path.Combine(wwwRootPath, @"images\photos");
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                newPhotoblogEntry.PhotoList.Last().ImageUrl = @"\images\photos\" + fileName + extension;

                //newPhotoblogEntry.PhotoList.Last().ImageUrl = Path.Combine(uploads, filename + extension;
            }
            
            // add to db
            _unitOfWork.PhotoblogEntry.Add(newPhotoblogEntry);
            _unitOfWork.Save();

            return RedirectToAction("Index");
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
