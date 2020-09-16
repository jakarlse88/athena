using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Stance : IEntityBase
    {
        public Stance()
        {
            Movement = new HashSet<Movement>();
        }

        public int Id { get; set; }
        public int StanceCategoryId { get; set; }
        public int StanceTypeId { get; set; }
        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual StanceCategory StanceCategory { get; set; }
        public virtual StanceType StanceType { get; set; }
        public virtual ICollection<Movement> Movement { get; set; }
    }
}
