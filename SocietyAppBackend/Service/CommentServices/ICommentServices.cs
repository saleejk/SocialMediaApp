using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.CommentServices
{
    public interface ICommentServices
    {
        Task<bool> AddComment(int userid, int postid, string text);
        Task<List<CommentDto>> GetAllComment();
        Task<CommentDto> GetCommentByid(int id);
        Task<List<CommentDto>> GetCommentByPostId(int postid);
        Task<bool> DeleteComment(int id); 
    }
}
