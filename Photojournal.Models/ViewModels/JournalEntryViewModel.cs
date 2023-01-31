using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoblog.Models.ViewModels
{
    public class JournalEntryViewModel
    {
        // Information for the set of photos
        public string PhotoSetTitle { get; set; }
        public string PhotoSetDescription { get; set; }
        public DateTime? PhotoSetDate { get; set; }
        // Information for individual photos
        [ValidateNever]
        public List<string> PhotoUrlList { get; set; }
        [ValidateNever]
        public float? PhotoLocationLat { get; set; }
        [ValidateNever]
        public float? PhotoLocationLng { get; set; }
    }
}
