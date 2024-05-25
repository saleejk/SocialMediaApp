using AutoMapper;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Mapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Post, PostViewDto>().ReverseMap();
        }
    }
}
