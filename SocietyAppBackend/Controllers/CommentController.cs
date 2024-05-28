using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddComment(int userid,int postid,string text)
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
        public async Task<IActionResult>GetCommentById(int id)
        {
            try
            {
                return Ok( await _commentServices.GetCommentByid(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
