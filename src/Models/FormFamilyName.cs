using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class FormFamilyName
    {
        public int FormFamilyId { get; set; }
        public int NameId { get; set; }

        public virtual FormFamily FormFamily { get; set; }
        public virtual Name Name { get; set; }
    }
}
