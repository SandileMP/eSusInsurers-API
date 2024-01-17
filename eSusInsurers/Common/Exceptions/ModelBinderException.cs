namespace eSusInsurers.Common.Exceptions
{
    public class ModelBinderException : Exception
    {
        public ModelBinderException()
            : base()
        {
        }

        public ModelBinderException(string message)
            : base(message)
        {
        }
    }
}
