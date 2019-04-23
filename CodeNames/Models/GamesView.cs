using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class GamesView
    {
        public int GameId { get; set; }
        public int WordId { get; set; }
        public short TeamId { get; set; }
        public short Order { get; set; }
        public bool? Find { get; set; }
        public string WordName { get; set; }
        public string ColorName { get; set; }
        public string BackgroundColorName { get; set; }
    }
}
