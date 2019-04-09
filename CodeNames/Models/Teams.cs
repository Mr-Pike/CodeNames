using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Teams
    {
        public Teams()
        {
            Gameswords = new HashSet<Gameswords>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Gameswords> Gameswords { get; set; }
    }
}
