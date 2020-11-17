using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using System;

namespace RugbyUnion.ManagementSystem.Models
{
    public class TeamStadiumDto : BaseDto<TeamStadium>
    {
        public int TeamId { get; set; }

        public int StadiumId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public override TeamStadium GetModel()
        {
            var model = new TeamStadium();

            if (Id.HasValue)
                model.Id = Id.Value;

            model.TeamId = TeamId;
            model.StadiumId = StadiumId;
            model.FromDate = FromDate;
            model.ToDate = ToDate;

            return model;
        }

        public override void PopulateModel(TeamStadium model)
        {
            TeamId = model.TeamId;
            StadiumId = model.StadiumId;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
        }
    }

}
