namespace TodoMinimalApi.Common
{
    /// <summary>
    /// Interface for entities with soft delete
    /// </summary>
    public interface IEntitySoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
