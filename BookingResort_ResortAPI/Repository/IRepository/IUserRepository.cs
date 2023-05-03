using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;

namespace BookingResort_ResortAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
