namespace Cantaoria.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entity);
        bool Delete(T entity);
        bool DeleteRange(List<T> datas);
        Task<bool> DeleteAsync(string id);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}
