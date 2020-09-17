using System.Collections.Generic;

namespace Athena.Models.NewEntities
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
