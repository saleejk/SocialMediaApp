using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocietyAppBackend.ModelEntity.Dto;
using SocietyAppBackend.Service.PostServices;

namespace SocietyAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly IPostServices _post;
        public PostController(IPostServices post)
        {
            _post = post;
        }
        [HttpGet("GetAllPosts")]
       
        public async Task<IActionResult> GetAllPost()
        {
            try
            {
                return Ok(await _post.GetAllPosts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _post.GetPostById(id);
            if (post == null)
            {
              return  BadRequest("invalid postId");
            }
            return Ok(post);
        }
        [HttpGet("GetAllPostByUserId")]
        public async Task<IActionResult> GetAllPostByUserId(int userid)
        {
            return Ok(await _post.GetAllPostByUserId(userid));
        }
        [Authorize]
        [HttpPost("AddPost")]
        public async Task<IActionResult>AddPost([FromForm]PostDto postdto,IFormFile image)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                var splitToken = token.Split(' ');
                var jwtToken = splitToken[1];
                

                await _post.AddPost(jwtToken,postdto, image);
                return Ok("posted successfully");
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("updatePost")]
        public async Task<IActionResult> EditPost(int postid, [FromBody] PostDto postdto)
        {
            return Ok(await _post.UpdatePost(postid, postdto));
        }


        [HttpDelete("DeletePost")]
        [Authorize]
        public async Task<IActionResult>DeletePost(int postid)
        {
            return Ok(await _post.DeletePost(postid));
        }
    }
}
