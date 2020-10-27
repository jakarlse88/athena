using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class TechniqueType
    {
        public TechniqueType()
        {
            Techniques = new HashSet<Technique>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Technique> Techniques { get; set; }
    }
}
