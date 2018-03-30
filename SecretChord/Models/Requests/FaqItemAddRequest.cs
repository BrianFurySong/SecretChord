using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class FaqItemAddRequest
    {
        [Required]
        [MaxLength(200)]
        public string Question { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Answer { get; set; }
    }
}