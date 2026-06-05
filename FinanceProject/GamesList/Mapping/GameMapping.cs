using AutoMapper;
using GamesList.DTO;
using GamesList.Models;

namespace GamesList.Mapping
{
    public class GameMapping : Profile
    {
        public GameMapping()
        {
            CreateMap<CreateGamesDto, Games>()
                .ForMember(g => g.Genre,
                option => option.Ignore());

            CreateMap<UpdateGamesDto, Games>()
                .ForMember(g => g.Genre,
                option => option.Ignore());

            CreateMap<Games, GamesListDto>()
                .ForMember(g => g.Genre,
                option => option.MapFrom(src => src.Genre.Select(n => n.Name).ToList()));
        }
    }
}
