namespace TodoMinimalApi.Common
{
    /// <summary>
    /// Base type for entities
    /// </summary>
    /// <typeparam name="PKType">Primary key type</typeparam>
    public class Entity<PKType>
    {
        public PKType Id { get; set; }
    }
}
