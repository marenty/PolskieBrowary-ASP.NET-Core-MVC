using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolskieBrowary.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:ss}", ApplyFormatInEditMode = true)]
        public DateTime CommentData { get; set; }
        [Required(ErrorMessage = "Podaj swój komentarz.")]
        [StringLength(250)]
        [Display(Name = "Twój komentarz")]
        public string Comment_text { get; set; }
        public string UserName { get; set; }
        public int CommentedBeerId { get; set; }

        public Beer CommentedBeer { get; set; }

    }
}
