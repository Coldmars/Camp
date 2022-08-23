namespace Camp.Common.Exceptions
{
    public class ValidateException : WebException
    {
        public ValidateException()
        {

        }

        public ValidateException(string message)
            : base(message)
        {

        }

        public ValidateException(string message, string messageCode)
            : base(message, messageCode)
        {

        }

        public ValidateException(string message, Exception inner)
            : base(message, inner)
        {

        } 
    }
}
