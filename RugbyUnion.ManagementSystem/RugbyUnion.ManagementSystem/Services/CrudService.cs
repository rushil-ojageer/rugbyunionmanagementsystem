using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Models.Interfaces;
using RugbyUnion.ManagementSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public class CrudService : ICrudService
    {
        private readonly RugbyUnionContext _context;

        public CrudService(RugbyUnionContext context)
        {
            _context = context;
        }

        public CrudService()
        {
        }

        public async Task<Dto> Get<Dto, Model>(int id) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            var set = _context.GetSet<Model>();

            var model = await set.FindAsync(id);

            if (model == null)
                throw new Exception($"Unable to find entity with Id {id}");

            return GetDto<Dto, Model>(model);
        }

        public async Task<PagedDto<Dto, Model>> GetAll<Dto, Model>(int offset, int pageSize) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            var set = _context.GetSet<Model>();
            var query = set;

            var totalItems = query.Count();

            var items = await query.OrderBy(x => x.Id)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            var dtos = items
                .Select(x => GetDto<Dto, Model>(x))
                .ToList();

            var pagedDto = new PagedDto<Dto, Model>
            {
                Items = dtos,
                TotalItems = totalItems,
                Offset = offset,
                Count = pageSize
            };

            return pagedDto;
        }

        public async Task<Dto> Create<Dto, Model>(Dto dto) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            var set = _context.GetSet<Model>();

            var newModel = dto.GetModel();
            newModel.Id = 0;
            
            await newModel.Validate(set);

            var model = await set.AddAsync(newModel);
            await _context.SaveChangesAsync();

            return GetDto<Dto, Model>(model.Entity);
        }

        public async Task<Dto> Update<Dto, Model>(Dto dto) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            if (!dto.Id.HasValue)
                throw new Exception($"'{nameof(dto.Id)}' field is required.");

            var set = _context.GetSet<Model>();
            var model = await set.FindAsync(dto.Id);

            if (model == null)
                throw new Exception($"Unable to find entity with Id {dto.Id}");

            var newModel = dto.GetModel();
            await newModel.Validate(set, true);

            model.Update(newModel);
            await _context.SaveChangesAsync();

            return GetDto<Dto, Model>(model);
        }

        public async Task<Dto> Delete<Dto, Model>(int id) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            var set = _context.GetSet<Model>();
            var model = await set.FindAsync(id);

            if (model == null)
                throw new Exception($"Unable to find entity with Id {id}");

            set.Remove(model);
            await _context.SaveChangesAsync();

            return GetDto<Dto, Model>(model);
        }

        private Dto GetDto<Dto, Model>(Model model) where Dto : IDto<Model> where Model : class, IDbEntity<Model>
        {
            var dto = Activator.CreateInstance<Dto>();
            dto.Populate(model);
            return dto;
        }
    }
}
