namespace TodoMinimalApi.Common.Exceptions
{
    /// <summary>
    /// Base exception for api
    /// </summary>
    public class ApplicationExceptionBase : Exception
    {
        public int HttpStatusCode { get; set; }
        public ApplicationExceptionBase(string? message, int statusCode) : base(message)
        {
            HttpStatusCode = statusCode;
        }

       
    }
}
