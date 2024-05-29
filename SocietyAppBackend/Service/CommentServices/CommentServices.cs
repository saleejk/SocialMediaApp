using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.CommentServices
{
    public class CommentServices:ICommentServices
    {
        public readonly DbContextClass _dbcontext;
        public readonly IMapper _mapper;
        public CommentServices(DbContextClass dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<bool>AddComment(int userid,int postid,string text)
        {
            var post = await _dbcontext.Posts.FirstOrDefaultAsync(i => i.UserId == userid && i.PostId == postid);
            if (post == null)
            {
                return false;
            }
            var comment = new Comment { UserId = userid, PostId = postid, Text = text, CreatedAt = DateTime.Now };
            await _dbcontext.Comments.AddAsync(comment);
            await _dbcontext.SaveChangesAsync();
            return true;

        }
        public async Task<List<CommentDto>> GetAllComment()
        {
            //List<CommentDto> commentlist = new List<CommentDto>();
            //var cmt = _dbcontext.Comments.ToList();
            //foreach (Comment c in cmt)
            //{
            //    commentlist.Add(new CommentDto
            //    {
            //        CommentId = c.CommentId,
            //        UserId = c.UserId,
            //        PostId = c.PostId,
            //        Text = c.Text,
            //        CreatedAt = c.CreatedAt
            //    });
            //}
            //return commentlist;
            var cmt = await _dbcontext.Comments.ToListAsync();
            var mappedList = _mapper.Map<List<CommentDto>>(cmt);
            return mappedList;
        }
        public async Task<bool>DeleteComment(int id)
        {
            var comment = await _dbcontext.Comments.FirstOrDefaultAsync(i => i.CommentId == id);
               if(comment == null)
            {
                return false;
            }
            _dbcontext.Comments.Remove(comment);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<List<CommentDto>>GetCommentByPostId(int postid)
        {
            var comments = await _dbcontext.Comments.Where(i => i.PostId == postid).ToListAsync();
            var mappedCmt = _mapper.Map<List<CommentDto>>(comments);
            if(comments == null)
            {
                return null;
            }
            return mappedCmt;
        }

        public async Task<CommentDto>GetCommentByid(int id)
        {
            var cmt = await _dbcontext.Comments.FirstOrDefaultAsync(i => i.CommentId == id);
            if (cmt != null) {
                var mappedcomment = _mapper.Map<CommentDto>(cmt);
                return mappedcomment;
            }
            return null;
            
        }
        //    public async Task<bool>DeleteComment(int commentId)
        //{
        //    var comment = await _dbcontext.Comments.FirstOrDefaultAsync(i => i.CommentId == commentId);
        //    if (comment == null)
        //    {
        //        return false;
        //    }
        //    _dbcontext.Comments.Remove(comment);
        //     await _dbcontext.SaveChangesAsync();
        //    return true;
        //}


    }
}


