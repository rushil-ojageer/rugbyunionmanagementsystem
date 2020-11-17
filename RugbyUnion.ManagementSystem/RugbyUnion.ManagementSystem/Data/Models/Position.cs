using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class Position : IDbEntity<Position>
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [Range(0, 15)]
        public int NumberAllowedInTeam { get; set; }
        
        public void Update(Position entity)
        {
            Name = entity.Name;
            NumberAllowedInTeam = entity.NumberAllowedInTeam;
        }

        public async Task Validate(DbSet<Position> dbSet, bool isUpdate = false)
        {
            if (isUpdate)
            {
                if (await dbSet.AnyAsync(x => x.Id != Id && x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)))
                    throw new Exception($"Position with name '{Name}' already exists.");

                return;
            }

            if (await dbSet.AnyAsync(x => x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception($"Position with name '{Name}' already exists.");
        }

    }
}
