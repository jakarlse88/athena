using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class Stance
    {
        public Stance()
        {
            Movements = new HashSet<Movement>();
        }

        public string Name { get; set; }
        public string StanceCategoryName { get; set; }
        public string StanceTypeName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual StanceCategory StanceCategoryNameNavigation { get; set; }
        public virtual StanceType StanceTypeNameNavigation { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
    }
}
