using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;

namespace BookingResort_ResortAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName== username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.Username.ToLower() && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return null;
            }

            //if user was found generate JWT Token
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new LocalUser
            {
                UserName = registrationRequestDTO.Username,
                Password = registrationRequestDTO.Password,
                Name = registrationRequestDTO.Name,
                Role = registrationRequestDTO.Role
            };
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
