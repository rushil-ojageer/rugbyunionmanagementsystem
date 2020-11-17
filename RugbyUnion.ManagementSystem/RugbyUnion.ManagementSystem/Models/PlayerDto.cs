using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RugbyUnion.ManagementSystem.Models
{
    public class PlayerDto : BaseDto<Player>
    {
        [Required]
        [StringLength(20, ErrorMessage = "'First Name' length can't be more than 20.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "'Last Name' length can't be more than 20.")]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, 250)]
        public decimal HeightCentimeters { get; set; }

        [Required]
        [Range(0, 300)]
        public decimal WeightKilograms { get; set; }

        [Required]
        public int CurrentPositionId { get; set; }

        public int? CurrentTeamId { get; set; }

        public override Player GetModel()
        {
            var model = new Player();
            
            if (Id.HasValue)
                model.Id = Id.Value;

            model.FirstName = FirstName;
            model.LastName = LastName;
            model.DateOfBirth = DateOfBirth;
            model.HeightCentimeters = HeightCentimeters;
            model.WeightKilograms = WeightKilograms;
            model.CurrentPositionId = CurrentPositionId;

            return model;
        }

        public override void PopulateModel(Player model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            DateOfBirth = model.DateOfBirth;
            HeightCentimeters = model.HeightCentimeters;
            WeightKilograms = model.WeightKilograms;
            CurrentPositionId = model.CurrentPositionId;
            CurrentTeamId = model.Teams?
                .SingleOrDefault(x => DateTime.Today >= x.FromDate && (!x.ToDate.HasValue || x.ToDate < DateTime.Today))?
                .TeamId;
        }
    }
}
