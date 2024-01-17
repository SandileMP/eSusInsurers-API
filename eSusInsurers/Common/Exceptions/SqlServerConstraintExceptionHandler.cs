using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Common.Exceptions
{
    public static class SqlServerConstraintExceptionHandler
    {
        public static void HandleDbUpdateExceptionForDuplicateKey(DbUpdateException ex)
        {
            if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
            {
                var errorMessage = sqlException.Message;

                // Parse the error message to identify the duplicate key value
                var startIndex = errorMessage.IndexOf("duplicate key value is (") + "duplicate key value is (".Length;
                var endIndex = errorMessage.IndexOf(")", startIndex);
                var duplicateKeyValue = errorMessage.Substring(startIndex, endIndex - startIndex);

                // Update the error message with the custom information
                var customErrorMessage = $"Duplicate key value found: {duplicateKeyValue}";

                throw new SqlDbUpdateException(customErrorMessage);
            }
            else
            {
                throw new SqlDbUpdateException(ex.Message, ex.InnerException ?? ex);
            }
        }
    }
}
