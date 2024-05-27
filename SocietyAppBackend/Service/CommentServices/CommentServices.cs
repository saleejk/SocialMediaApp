using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.CommentServices
{
    public class CommentServices
    {
        public readonly DbContextClass _dbcontext;
        public CommentServices(DbContextClass dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<bool>AddComment(int userid,int postid,string text)
        {
            var post = await _dbcontext.Posts.FirstOrDefaultAsync(i => i.UserId == userid && i.PostId == postid);
            if (post == null)
            {
                return false;
            }
            var comment = new Comment { UserId = userid, PostId = postid, Text = text, CreatedAt = DateTime.Now };
            _dbcontext.Comments.Add(comment);
            await _dbcontext.SaveChangesAsync();
            return true;

        }
        //public async Task<List<CommentDto>> GetAllComment()
        //{
        //    List<CommentDto>commentlist= new List<CommentDto>();
        //    var cmt = _dbcontext.Comments.ToList();
        //    foreach(Comment c in cmt)
        //    {
        //        commentlist.Add(new CommentDto
        //        {
        //            CommentId = Convert.ToInt32(c[""])
        //    }
        //}
        public async Task<bool>DeleteComment(int userid,int commentId)
        {
            var comment = await _dbcontext.Comments.FirstOrDefaultAsync(i => i.UserId == userid && i.CommentId == commentId);
            if (comment == null)
            {
                return false;
            }
            _dbcontext.Comments.Remove(comment);
             await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}


