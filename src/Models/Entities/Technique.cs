using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Technique : IEntityBase
    {
        public Technique()
        {
            Movement = new HashSet<Movement>();
            Transition = new HashSet<Transition>();
        }

        public int Id { get; set; }
        public int TechniqueTypeId { get; set; }
        public int TechniqueCategoryId { get; set; }
        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual TechniqueCategory TechniqueCategory { get; set; }
        public virtual TechniqueType TechniqueType { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
        public virtual ICollection<Transition> Transition { get; set; }
    }
}
