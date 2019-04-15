using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Games
    {
        public Games()
        {
            Gameswords = new HashSet<Gameswords>();
        }

        public int Id { get; set; }
        public short ScoreBlueTeam { get; set; }
        public short ScoreRedTeam { get; set; }
        public short RoundBlueTeam { get; set; }
        public short RoundRedTeam { get; set; }
        public short StartTeamId { get; set; }
        public short NextToPlayTeamId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Teams NextToPlayTeam { get; set; }
        public virtual Teams StartTeam { get; set; }
        public virtual ICollection<Gameswords> Gameswords { get; set; }
    }
}
