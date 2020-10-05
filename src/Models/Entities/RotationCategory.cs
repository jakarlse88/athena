using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class RotationCategory
    {
        public RotationCategory()
        {
            Transition = new HashSet<Transition>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual ICollection<Transition> Transition { get; set; }
    }
}
