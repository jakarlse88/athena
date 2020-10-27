#nullable disable

namespace Athena.Models.Entities
{
    public partial class TransitionCategoryTransition
    {
        public string TransitionCategoryName { get; set; }
        public int TransitionId { get; set; }

        public virtual Transition Transition { get; set; }
        public virtual TransitionCategory TransitionCategoryNameNavigation { get; set; }
    }
}
