using Data.Domain.Base;

namespace Repositories.Base
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        T Insert(T obj);
        void Update(T obj);
        void Delete(long id);
        void Save();
    }
}