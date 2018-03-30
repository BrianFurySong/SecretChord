using SecretChord.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class FaqItemUpdateRequest : FaqItemAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}