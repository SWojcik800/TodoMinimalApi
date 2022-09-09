namespace TodoMinimalApi.Common.Response
{
    /// <summary>
    /// Paginated elements response
    /// </summary>
    /// <typeparam name="T">Items type</typeparam>
    public class PaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
