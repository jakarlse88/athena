using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class TransitionCategory : IEntityBase
    {
        public TransitionCategory()
        {
            TransitionCategoryTransition = new HashSet<TransitionCategoryTransition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }
    }
}
