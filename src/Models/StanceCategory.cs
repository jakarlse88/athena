using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class StanceCategory
    {
        public StanceCategory()
        {
            Stance = new HashSet<Stance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stance> Stance { get; set; }
    }
}
