using System;
using System.Collections.Generic;

namespace CodeNames.Models
{
    public partial class Themes
    {
        public Themes()
        {
            Themeswords = new HashSet<Themeswords>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Themeswords> Themeswords { get; set; }
    }
}
