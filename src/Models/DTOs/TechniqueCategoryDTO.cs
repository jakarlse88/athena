namespace Athena.Models.DTOs
{
    public record TechniqueCategoryDTO
    {
        public string Name { get; init; }
        public string NameHangeul { get; init; }
        public string NameHanja { get; init; }
        public string Description { get; init; }

        public TechniqueCategoryDTO(string name, string nameHangeul, string nameHanja, string description) =>
            (Name, NameHangeul, NameHanja, Description) = (name, nameHangeul, nameHanja, description);

        public TechniqueCategoryDTO()
        {
        }
    }
}