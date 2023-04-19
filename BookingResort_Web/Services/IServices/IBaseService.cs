using BookingResort_Web.Models;

namespace BookingResort_Web.Services.IServices
{
	public interface IBaseService
	{
		APIResponse responseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
