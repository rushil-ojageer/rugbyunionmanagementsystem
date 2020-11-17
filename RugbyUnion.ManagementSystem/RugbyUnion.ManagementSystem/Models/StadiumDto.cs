using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RugbyUnion.ManagementSystem.Models
{
    public class StadiumDto : BaseDto<Stadium>
    {
        [Required]
        [StringLength(50, ErrorMessage = "'Name' length can't be more than 50.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "'Location' length can't be more than 50.")]
        public string Location { get; set; }

        [Required]
        [Range(0, 150000)]
        public int MaxOccupancy { get; set; }

        public int? CurrentTeamId { get; set; }

        public override Stadium GetModel()
        {
            var model = new Stadium();

            if (Id.HasValue)
                model.Id = Id.Value;

            model.Name = Name;
            model.Location = Location;
            model.MaxOccupancy = MaxOccupancy;

            return model;
        }

        public override void PopulateModel(Stadium model)
        {
            Name = model.Name;
            Location = model.Location;
            MaxOccupancy = model.MaxOccupancy;
            CurrentTeamId = model.Teams?
                .SingleOrDefault(x => DateTime.Today >= x.FromDate && (!x.ToDate.HasValue || x.ToDate < DateTime.Today))?
                .TeamId;
        }
    }
}
