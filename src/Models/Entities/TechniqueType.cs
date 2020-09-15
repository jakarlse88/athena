using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class TechniqueType : IEntityBase
    {
        public TechniqueType()
        {
            Technique = new HashSet<Technique>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Technique> Technique { get; set; }
    }
}
