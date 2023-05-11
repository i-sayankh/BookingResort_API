using BookingResort_Web.Models.DTO;

namespace BookingResort_Web.Services.IServices
{
	public interface IResortService
	{
		Task<T> GetAllAsync<T>(string token);
		Task<T> GetAsync<T>(int id, string token);
		Task<T> CreateAsync<T>(ResortCreateDTO dto, string token);
		Task<T> UpdateAsync<T>(ResortUpdateDTO dto, string token);
		Task<T> DeleteAsync<T>(int id, string token);	

	}
}
