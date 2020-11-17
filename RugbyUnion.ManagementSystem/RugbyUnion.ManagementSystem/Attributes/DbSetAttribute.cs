using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Attributes
{
    public class DbSetAttribute : Attribute
    {
        public DbSetAttribute(Type modelType)
        {
            ModelType = modelType;
        }

        public Type ModelType { get; }
    }
}
