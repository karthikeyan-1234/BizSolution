using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IBaseRepository<T,DB> where T : class
    {
        Task<T> AddAsync(T entry);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        void Update(T entry);
    }

    public class BaseRepository<T, DB> : IBaseRepository<T,DB> where T : class where DB : DbContext
    {
        readonly DB _db;
        readonly DbSet<T> table;

        public BaseRepository(DB db)
        {
            _db = db;
            table = _db.Set<T>();
        }

        public async Task<T> AddAsync(T entry)
        {
            var res = await table!.AddAsync(entry);
            return res.Entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res = await table!.FindAsync(id);
            return res!;
        }

        public async Task DeleteAsync(int id)
        {
            var res = await GetByIdAsync(id);
            table!.Remove(res);
        }

        public void Update(T entry)
        {
            _db.Entry(entry).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _db.Database.CommitTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db!.SaveChangesAsync();
        }
    }
}
