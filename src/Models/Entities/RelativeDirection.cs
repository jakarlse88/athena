using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class RelativeDirection : IEntityBase
    {
        public RelativeDirection()
        {
            Transition = new HashSet<Transition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transition> Transition { get; set; }
    }
}
