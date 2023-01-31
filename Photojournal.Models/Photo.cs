using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photoblog.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateUploaded { get; set; }
        [Required]
        [ValidateNever]
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? DateTaken { get; set; }
        public float? LocationTakenLat { get; set; }
        public float? LocationTakenLng { get; set; }
        public int JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        [ValidateNever]
        [Display(Name = "Journal Entry")]
        public JournalEntry JournalEntry { get; set; }

    }
}