
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models;

namespace SeriesApi.Services
{
    public class SeriesService : ISeriesService
    {
        private readonly AppDbContext _context;
        public SeriesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Series>> GetAllSeries(byte id = 0)
        {
            return await _context.Series
                .Where(s => s.CategoryId == id || id == 0)
                .OrderByDescending(s => s.Rate)
                .Include(s => s.Category).ToListAsync();
        }

        public async Task<Series> GetById(int id)
        {
            return await _context.Series.Include(s => s.Category).SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Series>> GetByCategoryId(byte id)
        {
            return await _context.Series.Include(s => s.Category)
                .Where(s => s.CategoryId == id).ToListAsync();
        }

        public async Task<Series> CreateSeries(Series series)
        {
            await _context.Series.AddAsync(series);
            _context.SaveChanges();
            return series;
        }

        public Series UpdateSeries(Series series)
        {
            _context.Series.Update(series);
            _context.SaveChanges();
            return series;
        }

        public Series DeleteSeries(Series series)
        {
            _context.Series.Remove(series);
            _context.SaveChanges();
            return series;
        }

       

        
    }
}
