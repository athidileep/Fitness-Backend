using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Common
{
    public class ErrorResponses
    {
        public bool Success { get; set; }
        public Error? Error { get; set; }
    }
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
