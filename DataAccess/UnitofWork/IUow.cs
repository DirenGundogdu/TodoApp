using DataAccess.Interfaces;
using Entities.Domains;
using System.Threading.Tasks;

namespace DataAccess.UnitofWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;

        Task SaveChanges();

    }
}
