using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Data.SeedData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RugbyUnion.ManagementSystem.Data
{
    public class RugbyUnionContext : DbContext
    {
        public RugbyUnionContext(DbContextOptions<RugbyUnionContext> options) :
            base(options)
        {

        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Stadium> Stadiums { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        public DbSet<TeamStadium> TeamStadiums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var seeder = new Seeder(modelBuilder);
            seeder.SeedData<Position>("Positions");
            seeder.SeedData<Team>("Teams");
            seeder.SeedData<Stadium>("Stadiums");
            seeder.SeedData<Player>("Players");
            seeder.SeedData<TeamPlayer>("TeamPlayers");
            seeder.SeedData<TeamStadium>("TeamStadiums");

            modelBuilder.Entity<Position>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Team>(entity => {
                entity.HasIndex(e => new { e.Name, e.Location }).IsUnique();
            });

            modelBuilder.Entity<Stadium>(entity => {
                entity.HasIndex(e => new { e.Name, e.Location }).IsUnique();
            });

            modelBuilder.Entity<Player>(entity => {
                entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            });

        }
    }
}
