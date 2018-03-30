using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Domain
{
    public class AboutPage
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }
        public string ContentText { get; set; }
        public string ImageURL { get; set; }
        public int PageQuantity { get; set; }
    }
}