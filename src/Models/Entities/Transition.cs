using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class Transition
    {
        public Transition()
        {
            Movements = new HashSet<Movement>();
            TransitionCategoryTransitions = new HashSet<TransitionCategoryTransition>();
        }

        public int Id { get; set; }
        public string RotationCategoryName { get; set; }
        public string RelativeDirectionName { get; set; }
        public string StanceName { get; set; }
        public string TechniqueName { get; set; }
        public string Description { get; set; }

        public virtual RelativeDirection RelativeDirectionNameNavigation { get; set; }
        public virtual RotationCategory RotationCategoryNameNavigation { get; set; }
        public virtual Technique TechniqueNameNavigation { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransitions { get; set; }
    }
}
