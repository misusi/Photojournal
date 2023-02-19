using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photojournal.Models.ViewModels
{
    public class JournalEntryViewModel
    {
        public JournalEntry JournalEntry { get; set; }
        // Information for the set of photos
        public DateTime? PhotoSetDate { get; set; }
        // Information for individual photos
        [ValidateNever]
        public float? PhotoLocationLat { get; set; }
        [ValidateNever]
        public float? PhotoLocationLng { get; set; }
    }
}
