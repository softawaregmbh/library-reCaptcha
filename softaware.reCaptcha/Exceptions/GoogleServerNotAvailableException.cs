using System;
using System.Net;

namespace softaware.reCaptcha.Exceptions
{
    [Serializable]
    public class GoogleServerNotAvailableException : Exception
    {
        public string UsedUrl { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public GoogleServerNotAvailableException()
        {
        }

        public GoogleServerNotAvailableException(string message) : base(message)
        {
        }

        public GoogleServerNotAvailableException(string url, HttpStatusCode statusCode)
        {
            this.UsedUrl = url;
            this.StatusCode = statusCode;
        }

        public GoogleServerNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}