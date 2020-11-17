using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Models.Interfaces
{
    public interface IDto<Model> where Model : class
    {
        int? Id { get; set; }

        Model GetModel();

        void Populate(Model model);
    }
}
