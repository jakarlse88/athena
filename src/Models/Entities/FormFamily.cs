using System.Collections.Generic;

#nullable disable

namespace Athena.Models.Entities
{
    public partial class FormFamily
    {
        public FormFamily()
        {
            Forms = new HashSet<Form>();
        }

        public string Name { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
    }
}
