using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class AboutPageUpdateRequest : AboutPageAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}