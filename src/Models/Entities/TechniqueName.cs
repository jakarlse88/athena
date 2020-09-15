using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class TechniqueName
    {
        public int TechniqueId { get; set; }
        public int NameId { get; set; }

        public virtual Name Name { get; set; }
        public virtual Technique Technique { get; set; }
    }
}
