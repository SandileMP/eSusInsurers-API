using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Common.Exceptions
{

    public class SqlDbUpdateException : DbUpdateException
    {
        protected SqlDbUpdateException() : base() { }
        public SqlDbUpdateException(string message)
            : base(message)
        {
        }

        public SqlDbUpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
