namespace ToursWebAppEXAMProject.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Map { get; set; }

        public List<CityDto> Cities { get; set; } = new();
    }
}
