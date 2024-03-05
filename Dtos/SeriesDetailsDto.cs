namespace SeriesApi.Dtos
{
    public class SeriesDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public int Episodes { get; set; }
        public int Seasons { get; set; }
        public byte[] Poster { get; set; }
        public byte CategoryId { get; set; }
        public string CategoryName {  get; set; }
    }
}
