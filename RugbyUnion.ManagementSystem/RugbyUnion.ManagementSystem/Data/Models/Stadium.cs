using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class Stadium : IDbEntity<Stadium>
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [Range(0, 150000)]
        public int MaxOccupancy { get; set; }

        public virtual List<TeamStadium> Teams { get; set; }

        public void Update(Stadium entity)
        {
            Name = entity.Name;
            Location = entity.Location;
            MaxOccupancy = entity.MaxOccupancy;
        }

        public async Task Validate(DbSet<Stadium> dbSet, bool isUpdate = false)
        {
            if (isUpdate)
            {
                if (await dbSet.AnyAsync(x => x.Id != Id && x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) && x.Location.Equals(Location, StringComparison.InvariantCultureIgnoreCase)))
                    throw new Exception($"Stadium with name '{Name}' and location '{Location}' already exists.");

                return;
            }

            if (await dbSet.AnyAsync(x => x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) && x.Location.Equals(Location, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception($"Stadium with name '{Name}' and location '{Location}' already exists.");
        }
    }
}
