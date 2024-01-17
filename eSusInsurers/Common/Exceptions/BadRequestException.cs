namespace eSusInsurers.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string? Url { get; set; }

        public BadRequestException()
            : base()
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, string url) : base(message)
        {
            Url = url;
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BadRequestException(string name, object key)
            : base("Your custome message")
        {
        }
    }
}
