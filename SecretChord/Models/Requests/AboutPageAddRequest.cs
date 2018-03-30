using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class AboutPageAddRequest
    {
        [Required]
        [MaxLength(50)]
        public string HeadLine { get; set; }
        [Required]
        [MaxLength(500)]
        public string ContentText { get; set; }
        [MaxLength(255)]
        public string ImageURL { get; set; }
    }
}