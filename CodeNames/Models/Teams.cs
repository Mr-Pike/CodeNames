using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Teams
    {
        public Teams()
        {
            GamesNextToPlayTeam = new HashSet<Games>();
            GamesStartTeam = new HashSet<Games>();
            Gameswords = new HashSet<Gameswords>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }

        public virtual ICollection<Games> GamesNextToPlayTeam { get; set; }
        public virtual ICollection<Games> GamesStartTeam { get; set; }
        public virtual ICollection<Gameswords> Gameswords { get; set; }
    }
}
