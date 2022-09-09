namespace TodoMinimalApi.Common.Response
{
    /// <summary>
    /// Base format of api response
    /// </summary>
    /// <typeparam name="T">Type of response data</typeparam>
    public class ApiResponse<T>
    {
        public ApiResponse() {}

        public ApiResponse(T data)
        {
            Data = data;
        }
        public T? Data { get; set; }
        public bool IsError { get; set; } = false;
        public string? ErrorMessage { get; set; }
        
    }
    public class ApiResponse
    {
        public bool IsError { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
