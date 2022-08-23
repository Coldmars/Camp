namespace Camp.Common.Exceptions
{
    public class WebException : Exception
    {
        public WebException()
        {

        }

        public WebException(string message)
            : base(message)
        {

        }

        public WebException(string message, string messageCode)
            : base(message)
        {
            MessageCode = messageCode;
        }

        public WebException(string message, Exception inner)
            : base(message, inner)
        {

        }

        public string MessageCode { get; set; }
    }
}
