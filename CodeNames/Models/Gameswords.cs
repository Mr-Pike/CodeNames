using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Gameswords
    {
        public int GameId { get; set; }
        public int WordId { get; set; }
        public short TeamId { get; set; }
        public short Order { get; set; }
        public bool? Find { get; set; }

        public virtual Games Game { get; set; }
        public virtual Teams Team { get; set; }
        public virtual Words Word { get; set; }
    }
}
