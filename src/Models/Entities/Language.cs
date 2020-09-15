using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Language : IEntityBase
    {
        public Language()
        {
            NameNavigation = new HashSet<Name>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Name> NameNavigation { get; set; }
    }
}
