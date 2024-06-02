using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SocietyAppBackend.Data;
using SocietyAppBackend.Service.CommentServices;

namespace SocietyAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ICommentServices _commentServices;
        public CommentController(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpPost("AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment(int userid, int postid, string text)
        {
            try
            {
                var status = await _commentServices.AddComment(userid, postid, text);
                return Ok(status);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllComment")]
        public async Task<IActionResult> GetAllComment()
        {
            try
            {
                return Ok(await _commentServices.GetAllComment());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCommentById")]
        [Authorize]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                return Ok(await _commentServices.GetCommentByid(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCommentByPostId")]
        [Authorize]
        public async Task<IActionResult> GeAllCommentByPostId(int postid)
        {
            try
            {
                return Ok(await _commentServices.GetCommentByPostId(postid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                return Ok(await _commentServices.DeleteComment(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
