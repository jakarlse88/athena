using System;
using System.Collections.Generic;

namespace Athena.Models.NewEntities
{
    public partial class FormFamily : IEntityBase
    {
        public FormFamily()
        {
            Form = new HashSet<Form>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }

        public virtual ICollection<Form> Form { get; set; }
    }
}
