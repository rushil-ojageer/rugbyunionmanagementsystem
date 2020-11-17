using RugbyUnion.ManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Models
{
    public class PagedDto<Dto, Model> where Dto : IDto<Model> where Model : class
    {
        public IEnumerable<Dto> Items { get; set; }
        public int TotalItems { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
    }
}
