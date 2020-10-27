#nullable disable


namespace Athena.Models.Entities
{
    public partial class NumberInSequence
    {
        public string FormName { get; set; }
        public int MovementId { get; set; }
        public byte OrdinalNumber { get; set; }

        public virtual Form FormNameNavigation { get; set; }
        public virtual Movement Movement { get; set; }
    }
}
