using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class StanceTypeName
    {
        public int StanceTypeId { get; set; }
        public int NameId { get; set; }

        public virtual Name Name { get; set; }
        public virtual StanceType StanceType { get; set; }
    }
}
