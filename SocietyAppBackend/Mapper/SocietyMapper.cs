using AutoMapper;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Mapper
{
    public class SocietyMapper:Profile
    {
        public SocietyMapper()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Post, PostViewDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Follow,FollowDto>().ReverseMap();
        }
    }
}
