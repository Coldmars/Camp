namespace Camp.Common.Exceptions
{
    public class ForbiddenException : WebException
    {
        public ForbiddenException()
        {

        }

        public ForbiddenException(string message)
            : base(message)
        {

        }

        public ForbiddenException(string message, string messageCode)
            : base(message, messageCode)
        {

        }

        public ForbiddenException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
