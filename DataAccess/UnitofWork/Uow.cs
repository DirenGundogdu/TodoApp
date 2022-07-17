using DataAccess.Context;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities.Domains;
using System;
using System.Threading.Tasks;

namespace DataAccess.UnitofWork
{
    public class Uow : IUow
    {
        private readonly TodoContext _context;

        public Uow(TodoContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
