using RugbyUnion.ManagementSystem.Data.Models.Interfaces;
using RugbyUnion.ManagementSystem.Models.Interfaces;

namespace RugbyUnion.ManagementSystem.Models.Base
{
    public abstract class BaseDto<Model> : IDto<Model> where Model : class, IDbEntity<Model>
    {
        public int? Id { get; set; }

        public abstract Model GetModel();

        public void Populate(Model model)
        {
            Id = model.Id;
            PopulateModel(model);
        }

        public abstract void PopulateModel(Model model);
    }
}
