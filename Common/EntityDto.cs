namespace TodoMinimalApi.Common
{
    /// <summary>
    /// Base type for dtos
    /// </summary>
    /// <typeparam name="PKType">Primary key type</typeparam>
    public class EntityDto<PKType>
    {
        public PKType Id { get; set; }
    }
}
