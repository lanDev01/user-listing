using AutoMapper;
using user_listing.Dto;
using user_listing.Models;

namespace user_listing.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<User, UserListDto>();
        }
    }
}
