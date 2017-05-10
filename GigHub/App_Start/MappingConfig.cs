using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingConfig
    {

        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {

                config.CreateMap<ApplicationUser, UserDto>();
                config.CreateMap<Gig, GigDto>();
                config.CreateMap<ApplicationUser, UserDto>();
                config.CreateMap<Genre, GenreDto>();
                config.CreateMap<Notification, NotificationDto>();
            });
        }

    }
}