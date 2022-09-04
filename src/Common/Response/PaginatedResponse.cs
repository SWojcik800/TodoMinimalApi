namespace TodoMinimalApi.Common.Response
{
    public class PaginatedResponse<T>
    {
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
