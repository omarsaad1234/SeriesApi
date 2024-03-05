using AutoMapper;

namespace SeriesApi.Helpers.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Series, SeriesDetailsDto>();
            CreateMap<SeriesDto, Series>().ForMember(m => m.Poster, opt => opt.Ignore());
        }
    }
}
