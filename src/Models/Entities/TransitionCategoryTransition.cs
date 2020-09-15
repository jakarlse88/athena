using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class TransitionCategoryTransition
    {
        public int TransitionId { get; set; }
        public int TransitionCategoryId { get; set; }

        public virtual Transition Transition { get; set; }
        public virtual TransitionCategory TransitionCategory { get; set; }
    }
}
