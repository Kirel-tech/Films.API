    using Films.Domain;
    using Films.DTOs;
    using AutoMapper;
    namespace Films.Core;

    /// <summary>
    /// Mapping Film for FilmDTO
    /// </summary>
    public class FilmMapper : Profile
    {
        /// <summary>
        /// Films constructor
        /// </summary>
        public FilmMapper()
        {
            CreateMap<Film, FilmDto>().ReverseMap();
            CreateMap<FilmCreateDto, Film>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // Игнорируем маппинг списка жанров здесь

            CreateMap<Film, FilmCreateDto>()
                .ForMember(dest => dest.GenreNames, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)));
        
            CreateMap<Film, FilmUpdateDto>().ReverseMap();
        }
    }