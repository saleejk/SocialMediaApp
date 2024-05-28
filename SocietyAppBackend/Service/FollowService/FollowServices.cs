using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.FollowService
{
    public class FollowServices:IFollowServices
    {
        public readonly DbContextClass _dbcontext;
        public readonly IMapper _mapper;
        public FollowServices(DbContextClass dbcontext,IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<string>FollowUser(int userid,int followingid)
        {
            var k = await _dbcontext.Follows.FirstOrDefaultAsync(i => i.FollowerId == userid && i.FollowingId == followingid);
            if (userid == followingid||k!=null)
            {
                return "cannot follow yourself";
            }

            var follow = new Follow { FollowerId = userid, FollowingId = followingid, CreatedAt = DateTime.Now };
            await _dbcontext.Follows.AddAsync(follow);
           await _dbcontext.SaveChangesAsync();
            return "followed successfully";

        }
        public async Task<List<FollowDto>>GetFollowingInUser(int userid)
        {
            var followers = await _dbcontext.Follows.Where(i => i.FollowerId == userid).ToListAsync();
            return _mapper.Map<List<FollowDto>>(followers);

        }
        public async Task<List<FollowDto>> GetAllFollowersInAUser(int userid)
        {
            var followers = await _dbcontext.Follows.Where(i => i.FollowingId == userid).ToListAsync();
            return _mapper.Map<List<FollowDto>>(followers);
        }

        public async Task<List<FollowDto>> GetAllfollowList()
        {
           return  _mapper.Map<List<FollowDto>>(_dbcontext.Follows.ToList());
        }
        public async Task<string> UnFollowUser(int userid, int unfollowingid)
        {
            var isAvailable = await _dbcontext.Follows.FirstOrDefaultAsync(i => i.FollowerId == userid && i.FollowingId == unfollowingid);
            if (isAvailable == null)
            {
                return "invalid operation";
            }
            else
            {
                _dbcontext.Follows.Remove(isAvailable);
                await _dbcontext.SaveChangesAsync();
                return "unfollow successfully";
            }
        }




    }
}
