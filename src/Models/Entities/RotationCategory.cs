using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class RotationCategory : IEntityBase
    {
        public RotationCategory()
        {
            Transition = new HashSet<Transition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transition> Transition { get; set; }
    }
}
