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
        public short ScoreAteam { get; set; }
        public short ScoreBteam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Gameswords> Gameswords { get; set; }
    }
}
