using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class RelativeDirection
    {
        public RelativeDirection()
        {
            Transitions = new HashSet<Transition>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Transition> Transitions { get; set; }
    }
}
