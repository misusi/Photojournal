using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photojournal.Models
{
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }
        //public int PhotoSetId { get; set;  }
        //[ForeignKey("PhotoSetId")]
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Photo> PhotoList { get; set; }
        public JournalEntry()
        {
            PhotoList= new List<Photo>();
        }
    }
}
