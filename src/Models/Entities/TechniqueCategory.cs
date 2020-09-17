using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class TechniqueCategory
    {
        public TechniqueCategory()
        {
            Technique = new HashSet<Technique>();
        }

        public string Name { get; set; }

        public virtual ICollection<Technique> Technique { get; set; }
    }
}
