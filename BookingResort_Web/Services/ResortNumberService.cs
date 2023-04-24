using BookingResort_Utility;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;

namespace BookingResort_Web.Services
{
	public class ResortNumberService : BaseService, IResortNumberService
    {
		private readonly IHttpClientFactory _clientFactory;
		private string resortUrl;
		public ResortNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) 
		{
			_clientFactory = clientFactory;
			resortUrl = configuration.GetValue<string>("ServiceUrls:ResortAPI");
		}

		public Task<T> CreateAsync<T>(ResortNumberCreateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url= resortUrl+ "/api/ResortNumberAPI"
            });
		}

		public Task<T> DeleteAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = resortUrl + "/api/ResortNumberAPI/" + id
			});
		}

		public Task<T> GetAllAsync<T>()
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = resortUrl + "/api/ResortNumberAPI"
            });
		}

		public Task<T> GetAsync<T>(int id)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = resortUrl + "/api/ResortNumberAPI/" + id
			});
		}

		public Task<T> UpdateAsync<T>(ResortNumberUpdateDTO dto)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = resortUrl + "/api/ResortNumberAPI/" + dto.ResortNo
			});
		}
	}
}
