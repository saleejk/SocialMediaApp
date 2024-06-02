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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _post.GetPostById(id);
                if (post == null)
                {
                    return BadRequest("invalid postId");
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
              return  StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllPostByUserId")]
        [Authorize]
        public async Task<IActionResult> GetAllPostByUserId(int userid)
        {
            try
            {
                return Ok(await _post.GetAllPostByUserId(userid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
        [Authorize]
        public async Task<IActionResult> EditPost(int postid, [FromBody] PostDto postdto)
        {
            try
            {
                return Ok(await _post.UpdatePost(postid, postdto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeletePost")]
        [Authorize]
        public async Task<IActionResult>DeletePost(int postid)
        {
            try
            {
                return Ok(await _post.DeletePost(postid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
