using BookingResort_Web.Models.DTO;

namespace BookingResort_Web.Services.IServices
{
	public interface IResortNumberService
    {
		Task<T> GetAllAsync<T>();
		Task<T> GetAsync<T>(int id);
		Task<T> CreateAsync<T>(ResortNumberCreateDTO dto);
		Task<T> UpdateAsync<T>(ResortNumberUpdateDTO dto);
		Task<T> DeleteAsync<T>(int id);	

	}
}
