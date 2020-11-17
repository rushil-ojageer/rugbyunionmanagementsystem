using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class Player : IDbEntity<Player>
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Range(0, 250)]
        public decimal HeightCentimeters { get; set; }

        [Range(0, 300)]
        public decimal WeightKilograms { get; set; }

        public int CurrentPositionId { get; set; }

        [ForeignKey("CurrentPositionId")]
        [Ignore]
        public virtual Position Position { get; set; }

        [Ignore]
        public virtual List<TeamPlayer> Teams { get; set; }

        public void Update(Player entity)
        {
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            DateOfBirth = entity.DateOfBirth;
            HeightCentimeters = entity.HeightCentimeters;
            WeightKilograms = entity.WeightKilograms;
            CurrentPositionId = entity.CurrentPositionId;
        }

        public async Task Validate(DbSet<Player> dbSet, bool isUpdate = false)
        {
            if (isUpdate)
            {
                if (await dbSet.AnyAsync(x => x.Id != Id && x.FirstName.Equals(FirstName, StringComparison.InvariantCultureIgnoreCase) && x.LastName.Equals(LastName, StringComparison.InvariantCultureIgnoreCase)))
                    throw new Exception($"Player with name '{FirstName} {LastName}' already exists.");

                return;
            }

            if (await dbSet.AnyAsync(x => x.FirstName.Equals(FirstName, StringComparison.InvariantCultureIgnoreCase) && x.LastName.Equals(LastName, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception($"Player with name '{FirstName} {LastName}' already exists.");
        }
    }
}
