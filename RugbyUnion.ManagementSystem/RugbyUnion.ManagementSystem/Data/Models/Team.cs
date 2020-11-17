using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class Team : IDbEntity<Team>
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        public virtual List<TeamPlayer> Players { get; set; }

        public virtual List<TeamStadium> Stadiums { get; set; }

        public void Update(Team entity)
        {
            Name = entity.Name;
            Location = entity.Location;
        }

        public bool CanAddPlayer(Player player, out string reason)
        {
            var positionId = player.CurrentPositionId;
            var maxPositionLimit = player.Position.NumberAllowedInTeam;
            var currentPositionCount = Players?.Count(x => x.PositionId == positionId) ?? 0;

            if (currentPositionCount >= maxPositionLimit)
            {
                reason = $"The team '{Name}' already has {currentPositionCount} players in the position '{player.Position.Name}'. The maximum as team can have for this position is {maxPositionLimit}.";
                return false;
            }

            reason = $"The team '{Name}' has {currentPositionCount} players in the position '{player.Position.Name}' which is less than the maximum as team can have for this position, which is {maxPositionLimit}.";
            return true;
        }

        public bool CanPlayInStadium(Stadium stadium, out string reason)
        {
            if (!stadium.Location.Equals(Location, StringComparison.InvariantCultureIgnoreCase))
            {
                reason = $"The location of team '{Name}' which is '{Location}' does not match the location of stadium '{stadium.Name}' which is '{stadium.Location}'";
                return false;
            }

            reason = $"The location of team '{Name}' which is '{Location}' matches the location of stadium '{stadium.Name}' which is '{stadium.Location}'";
            return true;
        }

        public async Task Validate(DbSet<Team> dbSet, bool isUpdate = false)
        {
            if (isUpdate)
            {
                if (await dbSet.AnyAsync(x => x.Id != Id && x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) && x.Location.Equals(Location, StringComparison.InvariantCultureIgnoreCase)))
                    throw new Exception($"Team with name '{Name}' and location '{Location}' already exists.");

                return;
            }

            if (await dbSet.AnyAsync(x => x.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase) && x.Location.Equals(Location, StringComparison.InvariantCultureIgnoreCase)))
                throw new Exception($"Team with name '{Name}' and location '{Location}' already exists.");
        }
    }
}
