namespace Athena.Models.DTOs
{
    public class TechniqueDTO
    {
        public string Name { get; set; }
        public string TechniqueTypeName { get; set; }
        public string TechniqueCategoryName { get; set; }
        public string NameHangeul { get; set; }
        public string NameHanja { get; set; }
        public string Description { get; set; }

        public TechniqueDTO(string name, string techniqueTypeName, string techniqueCategoryName, string nameHangeul,
            string nameHanja, string description) =>
            (Name, TechniqueTypeName, TechniqueCategoryName, NameHangeul, NameHanja, Description) = (name,
                techniqueTypeName, techniqueCategoryName, nameHangeul, nameHanja, description);

        public TechniqueDTO()
        {
        }
    }
}