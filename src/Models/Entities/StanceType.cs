using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class StanceType
    {
        public StanceType()
        {
            Stance = new HashSet<Stance>();
        }

        public string Name { get; set; }

        public virtual ICollection<Stance> Stance { get; set; }
    }
}
