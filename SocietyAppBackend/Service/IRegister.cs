using SocietyAppBackend.Data;
using SocietyAppBackend.ModelEntity;

namespace SocietyAppBackend.Service
{
    public interface IRegister
    {
        public void RegisterUser(UserDto userdto);
        public List<User> GetAllUsers();


    }
    public class RegisterService:IRegister
    {
        public readonly DbContextClass _dbcontext;
        public RegisterService(DbContextClass dbcontext)
        {
            _dbcontext= dbcontext;
            
        }
        public void RegisterUser(UserDto userdto)
        {
            _dbcontext.UserTable.Add(new User { Username=userdto.Username, Email=userdto.Email,PasswordHash=userdto.Password});
            _dbcontext.SaveChanges();
        }
        public List<User> GetAllUsers()
        {
           return _dbcontext.UserTable.ToList();
        }

    }
}
