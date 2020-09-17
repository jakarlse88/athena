using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class RelativeDirection
    {
        public RelativeDirection()
        {
            Transition = new HashSet<Transition>();
        }

        public string Name { get; set; }

        public virtual ICollection<Transition> Transition { get; set; }
    }
}
