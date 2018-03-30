using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class FileRepositoryAddRequest
    {
        [Required]
        [MaxLength(256)]
        public string FilePathName { get; set; }

    }
}