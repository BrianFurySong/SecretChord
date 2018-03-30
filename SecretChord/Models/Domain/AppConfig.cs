using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Domain
{
    public class AppConfig
    {
        public int Id { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
    }
}