using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class StanceCategory
    {
        public StanceCategory()
        {
            Stance = new HashSet<Stance>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual ICollection<Stance> Stance { get; set; }
    }
}
