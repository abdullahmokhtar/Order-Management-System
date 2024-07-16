namespace RouteSummitTask.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(params string[] includeProperties);
        Task<T?> GetByIdAsync(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
