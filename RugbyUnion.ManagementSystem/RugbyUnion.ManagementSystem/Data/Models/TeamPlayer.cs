using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Data.Models
{
    public class TeamPlayer : IDbEntity<TeamPlayer>
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }
        public int PositionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [Ignore]
        public virtual Team Team { get; set; }

        [Ignore]
        public virtual Player Player { get; set; }

        [Ignore]
        public virtual Position Position { get; set; }

        public void Update(TeamPlayer entity)
        {
        }

        public Task Validate(DbSet<TeamPlayer> dbSet)
        {
            return Task.CompletedTask;
        }

    }
}
