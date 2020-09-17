using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Transition
    {
        public Transition()
        {
            Movement = new HashSet<Movement>();
            TransitionCategoryTransition = new HashSet<TransitionCategoryTransition>();
        }

        public int Id { get; set; }
        public string RotationCategoryName { get; set; }
        public string RelativeDirectionName { get; set; }
        public string StanceName { get; set; }
        public string TechniqueName { get; set; }

        public virtual RelativeDirection RelativeDirectionNameNavigation { get; set; }
        public virtual RotationCategory RotationCategoryNameNavigation { get; set; }
        public virtual Technique TechniqueNameNavigation { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }
    }
}
