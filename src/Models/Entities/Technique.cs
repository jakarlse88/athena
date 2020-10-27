using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class Technique
    {
        public Technique()
        {
            Movements = new HashSet<Movement>();
            Transitions = new HashSet<Transition>();
        }

        public string Name { get; set; }
        public string TechniqueTypeName { get; set; }
        public string TechniqueCategoryName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual TechniqueCategory TechniqueCategoryNameNavigation { get; set; }
        public virtual TechniqueType TechniqueTypeNameNavigation { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
        public virtual ICollection<Transition> Transitions { get; set; }
    }
}
