using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class TransitionCategory
    {
        public TransitionCategory()
        {
            TransitionCategoryTransitions = new HashSet<TransitionCategoryTransition>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransitions { get; set; }
    }
}
