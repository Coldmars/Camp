namespace Camp.Common.Exceptions
{
    public class UnauthorizedException : WebException
    {
        public UnauthorizedException()
        {

        }

        public UnauthorizedException(string message)
            : base(message)
        {

        }

        public UnauthorizedException(string message, string messageCode)
            : base(message, messageCode)
        {

        }

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
