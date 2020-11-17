using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Attributes;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Utils
{
    public static class RugbyUnionContextExtensions
    {
        public static DbSet<Model> GetSet<Model>(this RugbyUnionContext context) where Model : class, IDbEntity<Model>
        {
            var type = typeof(Model);
            var set = context
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .SingleOrDefault(x => x.GetCustomAttribute<DbSetAttribute>() != null && x.GetCustomAttribute<DbSetAttribute>().ModelType == type);

            if (set == null)
                throw new Exception($"Unable to find property with type DbSet<{type.Name}> in {context.GetType().Name}.");

            var value = set.GetValue(context) as DbSet<Model>;

            return value;
        } 
    }
}
