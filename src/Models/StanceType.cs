using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class StanceType
    {
        public StanceType()
        {
            Stance = new HashSet<Stance>();
            StanceTypeName = new HashSet<StanceTypeName>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Stance> Stance { get; set; }
        public virtual ICollection<StanceTypeName> StanceTypeName { get; set; }
    }
}
