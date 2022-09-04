namespace TodoMinimalApi.Common.Exceptions
{
    public class ApplicationExceptionBase : Exception
    {
        public int HttpStatusCode { get; set; }
        public ApplicationExceptionBase(string? message, int statusCode) : base(message)
        {
            HttpStatusCode = statusCode;
        }

       
    }
}
