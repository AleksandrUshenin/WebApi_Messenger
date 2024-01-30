using AutoMapper;
using MessageManager.Models;
using MessageManager.Models.DTO;

namespace Messenger
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Message, MessageDTO>(MemberList.Destination).ReverseMap();
        }
    }
}
