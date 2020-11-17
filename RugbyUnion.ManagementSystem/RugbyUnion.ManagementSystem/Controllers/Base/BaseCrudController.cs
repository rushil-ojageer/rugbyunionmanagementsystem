using Microsoft.AspNetCore.Mvc;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Models.Interfaces;
using RugbyUnion.ManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Controllers.Base
{
    public class BaseCrudController<Dto, Model> : ControllerBase where Dto : IDto<Model> where Model : class, IDbEntity<Model>
    {
        private readonly ICrudService _crudService;

        public BaseCrudController(ICrudService crudService)
        {
            this._crudService = crudService;
        }

        [HttpGet("{id}")]
        public async Task<Dto> Get(int id)
        {
            return await _crudService.Get<Dto, Model>(id);
        }

        [HttpGet]
        public async Task<PagedDto<Dto, Model>> GetAll(int offset = 0, int pageSize = 10)
        {
            return await _crudService.GetAll<Dto, Model>(offset, pageSize);
        }

        [HttpPost]
        public async Task<Dto> Create(Dto dto)
        {
            return await _crudService.Create<Dto, Model>(dto);
        }

        [HttpPut]
        public async Task<Dto> Update(Dto dto)
        {
            return await _crudService.Update<Dto, Model>(dto);
        }

        [HttpDelete("{id}")]
        public async Task<Dto> Delete(int id)
        {
            return await _crudService.Delete<Dto, Model>(id);
        }
    }
}
