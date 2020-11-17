using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models.Base;
using RugbyUnion.ManagementSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Models
{
    public class TeamPlayerDto : BaseDto<TeamPlayer>
    {
        public int TeamId { get; set; }

        public int PlayerId { get; set; }

        public int PositionId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public override TeamPlayer GetModel()
        {
            var model = new TeamPlayer();

            if (Id.HasValue)
                model.Id = Id.Value;

            model.TeamId = TeamId;
            model.PlayerId = PlayerId;
            model.PositionId = PositionId;
            model.FromDate = FromDate;
            model.ToDate = ToDate;

            return model;

        }

        public override void PopulateModel(TeamPlayer model)
        {
            TeamId = model.TeamId;
            PlayerId = model.PlayerId;
            PositionId = model.PositionId;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
        }
    }
}
