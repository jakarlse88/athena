using System;
using System.Collections.Generic;

namespace Athena.Models
{
    public partial class Name
    {
        public Name()
        {
            FormFamilyName = new HashSet<FormFamilyName>();
            StanceTypeName = new HashSet<StanceTypeName>();
            TechniqueName = new HashSet<TechniqueName>();
        }

        public int Id { get; set; }
        public int NameCategoryId { get; set; }
        public int LanguageId { get; set; }
        public string Name1 { get; set; }

        public virtual Language Language { get; set; }
        public virtual NameCategory NameCategory { get; set; }
        public virtual ICollection<FormFamilyName> FormFamilyName { get; set; }
        public virtual ICollection<StanceTypeName> StanceTypeName { get; set; }
        public virtual ICollection<TechniqueName> TechniqueName { get; set; }
    }
}
