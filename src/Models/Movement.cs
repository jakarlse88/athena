using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Movement
    {
        public Movement()
        {
            FormMovement = new HashSet<FormMovement>();
        }

        public int Id { get; set; }
        public int StanceId { get; set; }
        public int TechniqueId { get; set; }
        public int TransitionId { get; set; }

        public virtual Stance Stance { get; set; }
        public virtual Technique Technique { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual ICollection<FormMovement> FormMovement { get; set; }
    }
}
