using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class FormFamily
    {
        public FormFamily()
        {
            Form = new HashSet<Form>();
            FormFamilyName = new HashSet<FormFamilyName>();
        }

        public int Id { get; set; }

        public virtual ICollection<Form> Form { get; set; }
        public virtual ICollection<FormFamilyName> FormFamilyName { get; set; }
    }
}
