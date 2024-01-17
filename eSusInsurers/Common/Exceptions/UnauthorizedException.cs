namespace eSusInsurers.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base()
        {
        }

        public UnauthorizedException(string message)
            : base(message)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnauthorizedException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
