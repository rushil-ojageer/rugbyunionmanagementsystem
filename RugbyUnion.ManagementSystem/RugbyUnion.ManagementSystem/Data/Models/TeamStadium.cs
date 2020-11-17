using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class TeamStadium : IDbEntity<TeamStadium>
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int StadiumId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [Ignore]
        public virtual Team Team { get; set; }

        [Ignore]
        public virtual Stadium Stadium { get; set; }

        public void Update(TeamStadium entity)
        {
        }

        public Task Validate(DbSet<TeamStadium> dbSet)
        {
            return Task.CompletedTask;
        }
    }
}
