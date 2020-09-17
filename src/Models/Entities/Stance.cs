using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Stance
    {
        public Stance()
        {
            Movement = new HashSet<Movement>();
        }

        public string Name { get; set; }
        public string StanceCategoryName { get; set; }
        public string StanceTypeName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual StanceCategory StanceCategoryNameNavigation { get; set; }
        public virtual StanceType StanceTypeNameNavigation { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
    }
}
