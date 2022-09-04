namespace TodoMinimalApi.Common.Exceptions
{
    public class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(string? message) : base(message, 404)
        {
        }
    }
}
