using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class Technique
    {
        public Technique()
        {
            Movement = new HashSet<Movement>();
            Transition = new HashSet<Transition>();
        }

        public string Name { get; set; }
        public string TechniqueTypeName { get; set; }
        public string TechniqueCategoryName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual TechniqueCategory TechniqueCategoryNameNavigation { get; set; }
        public virtual TechniqueType TechniqueTypeNameNavigation { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
        public virtual ICollection<Transition> Transition { get; set; }
    }
}
