using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Movement
    {
        public Movement()
        {
            NumberInSequence = new HashSet<NumberInSequence>();
        }

        public int Id { get; set; }
        public string StanceName { get; set; }
        public string TechniqueName { get; set; }
        public int TransitionId { get; set; }

        public virtual Stance StanceNameNavigation { get; set; }
        public virtual Technique TechniqueNameNavigation { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual ICollection<NumberInSequence> NumberInSequence { get; set; }
    }
}
