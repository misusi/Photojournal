using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoblog.Models
{
    public class PhotoblogEntry
    {
        [Key]
        public int Id { get; set; }
        //public int PhotoSetId { get; set;  }
        //[ForeignKey("PhotoSetId")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<Photo> PhotoList { get; set; }
        public PhotoblogEntry()
        {
            PhotoList= new List<Photo>();
        }
    }
}
