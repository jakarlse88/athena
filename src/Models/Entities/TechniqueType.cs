using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class TechniqueType
    {
        public TechniqueType()
        {
            Technique = new HashSet<Technique>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual ICollection<Technique> Technique { get; set; }
    }
}
