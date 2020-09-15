using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Form
    {
        public Form()
        {
            FormMovement = new HashSet<FormMovement>();
        }

        public int Id { get; set; }
        public int FormFamilyId { get; set; }

        public virtual FormFamily FormFamily { get; set; }
        public virtual ICollection<FormMovement> FormMovement { get; set; }
    }
}
