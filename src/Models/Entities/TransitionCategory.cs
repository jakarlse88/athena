using System.Collections.Generic;

namespace Athena.Models.Entities
{
    public partial class TransitionCategory
    {
        public TransitionCategory()
        {
            TransitionCategoryTransition = new HashSet<TransitionCategoryTransition>();
        }

        public string Name { get; set; }

        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }
    }
}
