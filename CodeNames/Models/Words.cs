using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Words
    {
        public Words()
        {
            Gameswords = new HashSet<Gameswords>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Gameswords> Gameswords { get; set; }
    }
}
