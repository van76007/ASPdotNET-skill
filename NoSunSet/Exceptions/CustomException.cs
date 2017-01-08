using System;

namespace NoSunSet.Exceptions
{
    public class CustomException : Exception
    {
        public String ErrorMessage { get; set; }

        public CustomException(String ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;
        }
    }
}