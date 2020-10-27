using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class StanceType
    {
        public StanceType()
        {
            Stances = new HashSet<Stance>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Stance> Stances { get; set; }
    }
}
