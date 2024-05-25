using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.PostServices
{
    public interface IPostServices
    {

        Task AddPost(string token, PostDto postdto, IFormFile image);
        Task<List<PostViewDto>> GetAllPosts();
         Task<PostViewDto> GetPostById(int id);



    }
}
