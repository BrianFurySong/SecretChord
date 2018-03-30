using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Requests
{
    public class FileRepositoryUpdateRequest : FileRepositoryAddRequest
    {
        public int Id { get; set; }
    }
}