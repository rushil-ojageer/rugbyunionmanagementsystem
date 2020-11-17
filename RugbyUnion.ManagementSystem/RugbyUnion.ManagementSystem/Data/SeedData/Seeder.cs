using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System.Globalization;
using System.IO;

namespace RugbyUnion.ManagementSystem.Data.SeedData
{
    public class Seeder
    {
        private readonly ModelBuilder _modelBuilder;

        public Seeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void SeedData<Model>(string file) where Model : class, IDbEntity<Model>
        {
            TextReader reader = new StreamReader($"Data/SeedData/Files/{file}.csv");
            var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var items = csvReader.GetRecords<Model>();
            _modelBuilder.Entity<Model>().HasData(items);
        }
    }
}
