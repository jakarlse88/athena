using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class RotationCategory
    {
        public RotationCategory()
        {
            Transition = new HashSet<Transition>();
        }

        public string Name { get; set; }

        public virtual ICollection<Transition> Transition { get; set; }
    }
}
