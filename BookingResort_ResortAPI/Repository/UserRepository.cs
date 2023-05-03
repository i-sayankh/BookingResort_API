using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;

namespace BookingResort_ResortAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
