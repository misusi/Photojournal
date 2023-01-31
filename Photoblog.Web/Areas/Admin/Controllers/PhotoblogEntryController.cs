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
        public PhotoblogEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public IActionResult Create(PhotoblogEntryViewModel photoblogEntryViewModel)
        {
            // create new photoblog entry from data in view model
            PhotoblogEntry newPhotoblogEntry = new();
            newPhotoblogEntry.Title = photoblogEntryViewModel.PhotoSetTitle;
            newPhotoblogEntry.Description = photoblogEntryViewModel.PhotoSetDescription;

            // create new photo object for each image url included
            foreach (string imgUrl in photoblogEntryViewModel.PhotoUrlList)
            {
                Photo newPhotoToAdd = new Photo
                {
                    ImageUrl = imgUrl,
                    Description = "", // TODO: Have one desc. for set or also per photo?
                    DateTaken = photoblogEntryViewModel.PhotoSetDate,
                    LocationTakenLat = photoblogEntryViewModel.PhotoLocationLat,
                    LocationTakenLng = photoblogEntryViewModel.PhotoLocationLng
                };
                newPhotoblogEntry.PhotoList.Add(newPhotoToAdd);

                // upload those photos to the db first
                //_unitOfWork.Photo.Add(newPhotoToAdd);
            }
            // add to db, etc...
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
