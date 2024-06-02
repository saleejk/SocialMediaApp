using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocietyAppBackend.Service.FollowService;

namespace SocietyAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        public readonly IFollowServices _followservices;
        public FollowController(IFollowServices followservices)
        {
            _followservices= followservices;
        }

        [HttpPost("FollowUser")]
        [Authorize]
        public async Task<IActionResult>FollowUser(int userid,int followingid)
        {
            try
            {
             
                return Ok(await _followservices.FollowUser(userid, followingid));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllfollowList")]
        [Authorize]
        public async Task<IActionResult> GetAllFollowList()
        {
            try
            {
                return Ok(await _followservices.GetAllfollowList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetFollowingsInAUser")]
        [Authorize]
        public async Task<IActionResult> GetFollowingInAUser(int userId)
        {
            try
            {
                return Ok(await _followservices.GetFollowingInUser(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllFollowersInAUser")]
        [Authorize]
        public async Task<IActionResult> GetAllFollowersinAUser(int userid)
        {
            try
            {
                return Ok(await _followservices.GetAllFollowersInAUser(userid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("UnfollowUser")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser(int userid,int unfollowId)
        {
            try
            {
                return Ok(await _followservices.UnFollowUser(userid, unfollowId));

            }
            catch (Exception ex) { return BadRequest(ex.Message);
            } 
        }
    }
}
