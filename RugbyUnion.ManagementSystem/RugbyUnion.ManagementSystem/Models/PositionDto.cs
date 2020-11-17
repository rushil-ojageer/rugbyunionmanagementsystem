using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace RugbyUnion.ManagementSystem.Models
{
    public class PositionDto : BaseDto<Position>
    {
        [Required]
        [StringLength(20, ErrorMessage = "'Name' length can't be more than 20.")]
        public string Name { get; set; }

        [Required]
        [Range(0, 15)]
        public int NumberAllowedInTeam { get; set; }

        public override Position GetModel()
        {
            var model = new Position();

            if (Id.HasValue)
                model.Id = Id.Value;

            model.Name = Name;
            model.NumberAllowedInTeam = NumberAllowedInTeam;

            return model;
        }

        public override void PopulateModel(Position model)
        {
            Name = model.Name;
            NumberAllowedInTeam = model.NumberAllowedInTeam;
        }
    }
}
