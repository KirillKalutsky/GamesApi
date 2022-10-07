using GamesApi.Models;

namespace GamesApi.DB
{
    public interface ICRUDRepository<T>
    {
        Task InsertAsync(T obj);
        Task<bool> DeleteAsync(Guid id);

        Task<PageList<T>> GetAsync(int currentPage, int pageSize);

        Task<T> GetAsync(Guid id);

        Task UpdateAsync(T obj);

        Task SaveAsync();

    }
}
