using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public interface ICrudService 
    {
        Task<Dto> Create<Dto, Model>(Dto dto) where Dto : IDto<Model> where Model : class, IDbEntity<Model>;

        Task<Dto> Update<Dto, Model>(Dto dto) where Dto : IDto<Model> where Model : class, IDbEntity<Model>;

        Task<Dto> Delete<Dto, Model>(int id) where Dto : IDto<Model> where Model : class, IDbEntity<Model>;

        Task<Dto> Get<Dto, Model>(int id) where Dto : IDto<Model> where Model : class, IDbEntity<Model>;

        Task<PagedDto<Dto, Model>> GetAll<Dto, Model>(int offset, int pageSize) where Dto : IDto<Model> where Model : class, IDbEntity<Model>;
    }
}
