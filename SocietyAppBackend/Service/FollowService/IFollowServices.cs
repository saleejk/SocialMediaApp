using SocietyAppBackend.ModelEntity;
using SocietyAppBackend.ModelEntity.Dto;

namespace SocietyAppBackend.Service.FollowService
{
    public interface IFollowServices
    {
        Task<string> FollowUser(int followerid, int followingid);
        Task<string> UnFollowUser(int followerid, int followingid);
         Task<List<FollowDto>> GetAllfollowList();
        Task<List<FollowDto>> GetFollowingInUser(int userid);
        Task<List<FollowDto>> GetAllFollowersInAUser(int userid);




    }
}
