using Applications.WebClient.Responses;
using System;

namespace Applications.WebClient.Helpers
{
    public static class ResponseHelper
    {
        public static ErrorResponse ClientErrorResponse(string errorMessage)
        {
            return new ErrorResponse(errorMessage);
        }
        public static ErrorResponse ClientErrorResponse(string errorMessage, Exception innerException)
        {
            return new ErrorResponse(errorMessage, innerException);
        }
    }
}