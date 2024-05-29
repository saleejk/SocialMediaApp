using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;

namespace SocietyAppBackend.Service.LikeService
{
    public class LikeServices:ILikeServices
    {
        public readonly DbContextClass _dbcontext;
        public LikeServices(DbContextClass dbcontext)
        {
            _dbcontext= dbcontext;
           
        }
        public async Task<bool> LikePost(int userId,int postId)
        {

            var existingLike = await _dbcontext.Likes.FirstOrDefaultAsync(i => i.UserId == userId && i.PostId == postId);
            if (existingLike != null)
            {
                return false;
            }
                var likes=new Like { UserId=userId, PostId=postId,CreatedAt=DateTime.Now };
                 _dbcontext.Likes.Add(likes);
                await _dbcontext.SaveChangesAsync();
                return true;
        }
        public async Task<bool>UnLikePost(int userid, int postid)
        {
            var isLikedorNot=await _dbcontext.Likes.FirstOrDefaultAsync(like=>like.UserId==userid&&like.PostId==postid);
            if (isLikedorNot == null)
            {
                return false;
            }
            _dbcontext.Likes.Remove(isLikedorNot);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<List<Like>> GetAllLikeByPostId(int postid)
        {
            var post = await _dbcontext.Likes.Where(i => i.PostId == postid).ToListAsync();
            if (post == null)
            {
                return null;
            }
            return post;
        }
           
    }
}
