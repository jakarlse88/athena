using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class Form : IEntityBase
    {
        public Form()
        {
            FormMovement = new HashSet<FormMovement>();
        }

        public int Id { get; set; }
        public int FormFamilyId { get; set; }
        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual FormFamily FormFamily { get; set; }
        public virtual ICollection<FormMovement> FormMovement { get; set; }
    }
}
