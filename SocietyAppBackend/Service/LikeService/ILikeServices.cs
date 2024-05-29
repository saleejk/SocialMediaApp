using SocietyAppBackend.ModelEntity;

namespace SocietyAppBackend.Service.LikeService
{
    public interface ILikeServices
    {
         Task<bool> LikePost(int userId, int postId);
         Task<bool> UnLikePost(int userid, int postid);
        Task<List<Like>> GetAllLikeByPostId(int postid);



    }
}
