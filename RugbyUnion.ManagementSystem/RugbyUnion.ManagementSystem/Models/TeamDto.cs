using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RugbyUnion.ManagementSystem.Models
{
    public class TeamDto : BaseDto<Team>
    {
        [Required]
        [StringLength(50, ErrorMessage = "'Name' length can't be more than 50.")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "'Location' length can't be more than 50.")]
        public string Location { get; set; }

        public int? CurrentStadiumId { get; set; }

        public List<TeamPlayerDto> CurrentPlayers { get; set; }

        public override Team GetModel()
        {
            var model = new Team();

            if (Id.HasValue)
                model.Id = Id.Value;

            model.Name = Name;
            model.Location = Location;

            return model;
        }

        public override void PopulateModel(Team model)
        {
            Name = model.Name;
            Location = model.Location;

            CurrentStadiumId = model.Stadiums?
                .SingleOrDefault(x => DateTime.Today >= x.FromDate && (!x.ToDate.HasValue || x.ToDate < DateTime.Today))?
                .StadiumId;

            CurrentPlayers = model.Players?
                .Where(x => DateTime.Today >= x.FromDate && (!x.ToDate.HasValue || x.ToDate < DateTime.Today))
                .Select(x => 
                {
                    var teamPlayerDto = new TeamPlayerDto();
                    teamPlayerDto.Populate(x);
                    return teamPlayerDto;
                })
                .ToList();
        }
    }
}
