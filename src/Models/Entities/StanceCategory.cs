using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class StanceCategory
    {
        public StanceCategory()
        {
            Stance = new HashSet<Stance>();
        }

        public string Name { get; set; }

        public virtual ICollection<Stance> Stance { get; set; }
    }
}
