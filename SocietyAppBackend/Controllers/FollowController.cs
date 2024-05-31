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
            return Ok(await _followservices.GetAllfollowList());
        }
        [HttpGet("GetFollowingsInAUser")]
        [Authorize]

        public async Task<IActionResult> GetFollowingInAUser(int userId)
        {
            return Ok(await _followservices.GetFollowingInUser(userId));
        }
        [HttpGet("GetAllFollowersInAUser")]
        [Authorize]

        public async Task<IActionResult> GetAllFollowersinAUser(int userid)
        {
            return Ok( await _followservices.GetAllFollowersInAUser(userid));
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
