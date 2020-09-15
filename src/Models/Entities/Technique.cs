using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Technique : IEntityBase
    {
        public Technique()
        {
            Movement = new HashSet<Movement>();
            TechniqueName = new HashSet<TechniqueName>();
        }

        public int Id { get; set; }
        public int TechniqueTypeId { get; set; }
        public int TechniqueCategoryId { get; set; }

        public virtual TechniqueCategory TechniqueCategory { get; set; }
        public virtual TechniqueType TechniqueType { get; set; }
        
        public virtual ICollection<Movement> Movement { get; set; }
        public virtual ICollection<TechniqueName> TechniqueName { get; set; }
    }
}
