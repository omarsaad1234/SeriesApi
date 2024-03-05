using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi.Models;
using SeriesApi.Services;

namespace SeriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISeriesService _seriesService;
        private readonly ICategoryService _categoryService;
        private readonly List<string> _allowedExtentions = new List<string> { ".jpg", ".png" };
        private readonly long _allowedFileSize = 1048576;
        public SeriesController(ISeriesService seriesServic, ICategoryService categoryService, IMapper mapper)
        {
            _seriesService = seriesServic;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Series = await _seriesService.GetAllSeries();
            var dataToView = _mapper.Map<IEnumerable<SeriesDetailsDto>>(Series);
            return Ok(dataToView);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Series = await _seriesService.GetById(id);
            if(Series is null)
            {
                return NotFound();
            }
            var dataToView = _mapper.Map<SeriesDetailsDto>(Series);
            return Ok(dataToView);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte CategoryId)
        {
            var isValidCategory = await _categoryService.isValidCategory(CategoryId);

            if (!isValidCategory)
                return BadRequest("Please Select An Existing Category Id");

            var Series = await _seriesService.GetAllSeries(CategoryId);

            var dataToView = _mapper.Map<IEnumerable<SeriesDetailsDto>>(Series);

            return Ok(dataToView);
        }

        [HttpPost]
        public async Task<IActionResult> PostSeriesAsync([FromForm]SeriesDto series)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(series.Poster.FileName).ToLower()))
                return BadRequest("Only '.jpg' and '.png' Extentions Allowed");

            if(series.Poster.Length>_allowedFileSize)
                return BadRequest("Max Allowed Size Is 1 Mb");

            var isValidCategory = await _categoryService.isValidCategory(series.CategoryId);

            if (!isValidCategory)
                return BadRequest("Please Select An Existing Category Id");

            using var dataStream = new MemoryStream();

            await series.Poster.CopyToAsync(dataStream);

            var seriesToAdd = _mapper.Map<Series>(series);

            seriesToAdd.Poster = dataStream.ToArray();
            
            await _seriesService.CreateSeries(seriesToAdd);
            
            return Ok(seriesToAdd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeriesAsync(int id,[FromForm]SeriesDto series)
        {

            var EditedSeries = await _seriesService.GetById(id);

            if (EditedSeries is null)
                return NotFound();

            var isValidCategory = await _categoryService.isValidCategory(series.CategoryId);

            if (!isValidCategory)
                return BadRequest("Please Select An Existing Category Id");

            if (series.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(series.Poster.FileName).ToLower()))
                    return BadRequest("Only '.jpg' and '.png' Extentions Allowed");

                if (series.Poster.Length > _allowedFileSize)
                    return BadRequest("Max Allowed Size Is 1 Mb");

                using var dataStream = new MemoryStream();

                await series.Poster.CopyToAsync(dataStream);
                
                EditedSeries.Poster = dataStream.ToArray();
            }

            EditedSeries.CategoryId = series.CategoryId;

            EditedSeries.Episodes = series.Episodes;
            
            EditedSeries.Rate = series.Rate;
            
            EditedSeries.Title = series.Title;
            
            EditedSeries.Seasons = series.Seasons;
            
            EditedSeries.Year = series.Year;

            _seriesService.UpdateSeries(EditedSeries);

            return Ok(EditedSeries);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var DeletedSeries = await _seriesService.GetById(id);
            if(DeletedSeries is null)
            {
                return NotFound();
            }
            _seriesService.DeleteSeries(DeletedSeries);
            return Ok(DeletedSeries);
        }

    }
}
