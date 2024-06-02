using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyAppBackend.Data;
using SocietyAppBackend.JwtVerification;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.PostServices
{
    public class PostServices : IPostServices
    {
        public readonly DbContextClass _dbcontext;
        public readonly IJwtService _jwtService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly IMapper _mapper;
        public PostServices(DbContextClass dbcontext, IJwtService jwtserices, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _jwtService = jwtserices;
            _webHostEnvironment = webHostEnvironment;
            _mapper=mapper;
        }
        public async Task<List<PostViewDto>> GetAllPosts()
        {
            try
            {
                var PostData = await _dbcontext.Posts.Include(i => i.User).ToListAsync();
                if (PostData.Count > 0)
                {
                    var productWithCategory = PostData.Select(p => new PostViewDto { PostId = p.PostId, UserId = p.UserId, ImageUrl = p.ImageUrl, CreatedAt = p.CreatedAt, Caption = p.Caption }).ToList();
                    return productWithCategory;
                }
                var k = _mapper.Map<List<PostViewDto>>(PostData);
                return k;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public async Task<PostViewDto> GetPostById(int id)
        {
            try
            {
                var k = await _dbcontext.Posts.FirstOrDefaultAsync(i => i.PostId == id);
                if (k == null)
                {
                    return null;
                }
                var postview = new PostViewDto { PostId = k.PostId, UserId = k.UserId, ImageUrl = k.ImageUrl, CreatedAt = k.CreatedAt, Caption = k.Caption };
                return postview;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task AddPost(string token, PostDto postdto, IFormFile image)
        {
            try
            {
                string productImage = null;
                if (image != null && image.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", "Post", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    productImage = "/Uploads/Products/" + fileName;
                }
                else
                {
                    productImage = "Uploads/Common/IMG_4741.jpeg";
                }
                var userId = _jwtService.GetUserIdFromToken(token);
                await _dbcontext.Posts.AddAsync(new Post {UserId=userId,ImageUrl=productImage, Caption=postdto.Caption,CreatedAt=DateTime.Now });
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding product:" + ex.Message);
            }
        }
        public async Task<List<PostViewDto>>GetAllPostByUserId(int userId)
        {
            try
            {
                var posts = _dbcontext.Posts.Where(i => i.UserId == userId);
                if (posts == null)
                {
                    return null;
                }
                var postview = _mapper.Map<List<PostViewDto>>(posts);
                return postview;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdatePost(int postid,[FromBody] PostDto postdto)
        {
            try
            {
                var editPost = await _dbcontext.Posts.FirstOrDefaultAsync(i => i.PostId == postid);
                if (editPost == null)
                {
                    return "invalid postid";
                }
                editPost.Caption = postdto.Caption;
                editPost.CreatedAt = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
                return "update Completed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> DeletePost(int postid)
        {
            try
            {
                var deletepost = await _dbcontext.Posts.FirstOrDefaultAsync(i => i.PostId == postid);
                if (deletepost == null)
                {
                    return "invalid postid";
                }
                _dbcontext.Posts.Remove(deletepost);
                await _dbcontext.SaveChangesAsync();
                return "deletion completed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

