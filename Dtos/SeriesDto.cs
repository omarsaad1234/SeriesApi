namespace SeriesApi.Dtos
{
    public class SeriesDto
    {
        [MaxLength(255)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public int Episodes { get; set; }
        public int Seasons { get; set; }
        public IFormFile Poster { get; set; }
        public byte CategoryId { get; set; }
    }
}
