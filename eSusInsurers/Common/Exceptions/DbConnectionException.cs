namespace eSusInsurers.Common.Exceptions
{
    public class DbConnectionException : Exception
    {
        protected DbConnectionException() : base() { }
        protected DbConnectionException(string message)
            : base(message)
        {
        }

        public DbConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
