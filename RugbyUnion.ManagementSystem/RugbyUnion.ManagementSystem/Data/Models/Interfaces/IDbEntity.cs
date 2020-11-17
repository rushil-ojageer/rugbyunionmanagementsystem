using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models.Interfaces
{
    public interface IDbEntity<Model> where Model : class
    {
        int Id { get; set; }

        void Update(Model entity);

        Task Validate(DbSet<Model> dbSet);
    }
}
