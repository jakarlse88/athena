using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class Form
    {
        public Form()
        {
            NumberInSequences = new HashSet<NumberInSequence>();
        }

        public string Name { get; set; }
        public string FormFamilyName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual FormFamily FormFamilyNameNavigation { get; set; }
        public virtual ICollection<NumberInSequence> NumberInSequences { get; set; }
    }
}
