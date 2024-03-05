using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesApi.Models
{
    public class Series
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public int Year {  get; set; }
        public double Rate {  get; set; }
        public int Episodes {  get; set; }
        public int Seasons {  get; set; }
        public byte[] Poster {  get; set; }
        public byte CategoryId { get; set; }
        public Category Category {  get; set; }


    }
}
