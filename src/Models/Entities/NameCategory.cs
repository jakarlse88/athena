using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class NameCategory : IEntityBase
    {
        public NameCategory()
        {
            Name = new HashSet<Name>();
        }

        public int Id { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Name> Name { get; set; }
    }
}
