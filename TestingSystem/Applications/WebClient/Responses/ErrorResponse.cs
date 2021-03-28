using System;

namespace Applications.WebClient.Responses
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }

        public ErrorResponse()
        {

        }

        public ErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public ErrorResponse(string errorMessage, Exception innerException = null)
        {
            ErrorMessage = errorMessage;
            InnerException = innerException == null ? "null" : innerException.ToString();
        }
    }
}
