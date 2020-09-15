using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Transition : IEntityBase
    {
        public Transition()
        {
            Movement = new HashSet<Movement>();
            TransitionCategoryTransition = new HashSet<TransitionCategoryTransition>();
        }

        public int Id { get; set; }
        public int RotationCategoryId { get; set; }
        public int RelationDirectionId { get; set; }
        public int StanceId { get; set; }

        public virtual RelativeDirection RelationDirection { get; set; }
        public virtual RotationCategory RotationCategory { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
        public virtual ICollection<TransitionCategoryTransition> TransitionCategoryTransition { get; set; }
    }
}
