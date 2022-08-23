namespace Camp.Common.Exceptions
{
    public class NotFoundException : WebException
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
            : base(message)
        {

        }

        public NotFoundException(string message, string messageCode)
            : base(message, messageCode)
        {

        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
