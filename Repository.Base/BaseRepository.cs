using Data.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Base
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private DbContext _context = null;
        private DbSet<T> _table = null;
        public BaseRepository()
        {
            _context = new MainContext();
            _table = _context.Set<T>();
        }
        public BaseRepository(DbContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }
        public void Delete(long id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }
        public T GetById(long id)
        {
            return _table.Find(id);
        }
        public T Insert(T obj)
        {
            _table.Add(obj);
            return obj;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
