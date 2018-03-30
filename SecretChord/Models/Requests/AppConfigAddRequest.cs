using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class AppConfigAddRequest
    {
        [Required]
        [MaxLength(100)]
        public string ConfigKey { get; set; }
        [Required]
        [MaxLength(300)]
        public string ConfigValue { get; set; }
    }
}