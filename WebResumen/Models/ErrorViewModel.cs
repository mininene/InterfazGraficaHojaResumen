using System;

namespace WebResumen.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorStatusCode { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
