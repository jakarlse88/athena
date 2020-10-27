using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class Movement
    {
        public Movement()
        {
            NumberInSequences = new HashSet<NumberInSequence>();
        }

        public int Id { get; set; }
        public string StanceName { get; set; }
        public string TechniqueName { get; set; }
        public int TransitionId { get; set; }
        public string Description { get; set; }

        public virtual Stance StanceNameNavigation { get; set; }
        public virtual Technique TechniqueNameNavigation { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual ICollection<NumberInSequence> NumberInSequences { get; set; }
    }
}
