using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Domain
{
    public class FaqItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}