using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        #region Constructor
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }
        #endregion
    }
}
