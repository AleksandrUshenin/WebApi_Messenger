using AutoMapper;
using Messenger.Models;
using Messenger.Models.DTO;

namespace Messenger
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserDTO>(MemberList.Destination).ReverseMap();
            CreateMap<LoginUser, UserDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
