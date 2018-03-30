using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretChord.Models.Responses
{
    public class ResponseMsg
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }

        public ResponseMsg()
        {

        }

        public ResponseMsg(string message, bool isSuccessful)
        {
            Message = message;
            IsSuccessful = isSuccessful;
        }
    }
}