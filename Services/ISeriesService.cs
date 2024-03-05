namespace SeriesApi.Services
{
    public interface ISeriesService
    {
        Task<IEnumerable<Series>> GetAllSeries(byte id = 0);

        Task<Series> GetById(int id);

        Task<IEnumerable<Series>> GetByCategoryId(byte id);

        Task<Series> CreateSeries(Series series);

        Series UpdateSeries(Series series);

        Series DeleteSeries(Series series);
    }
}
