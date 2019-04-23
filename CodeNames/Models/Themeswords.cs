using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Themeswords
    {
        public int ThemeId { get; set; }
        public int WordId { get; set; }

        public virtual Themes Theme { get; set; }
        public virtual Words Word { get; set; }
    }
}
