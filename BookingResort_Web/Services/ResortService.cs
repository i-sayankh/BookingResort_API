using BookingResort_Utility;
using BookingResort_Web.Models;
using BookingResort_Web.Models.DTO;
using BookingResort_Web.Services.IServices;

namespace BookingResort_Web.Services
{
	public class ResortService : BaseService, IResortService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string resortUrl;
		public ResortService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory) 
		{
			_clientFactory = clientFactory;
			resortUrl = configuration.GetValue<string>("ServiceUrls:ResortAPI");
		}

		public Task<T> CreateAsync<T>(ResortCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url= resortUrl+ "/api/v1/ResortAPI",
                Token = token
            });
		}

		public Task<T> DeleteAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = resortUrl + "/api/v1/ResortAPI/" + id,
                Token = token
            });
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = resortUrl + "/api/v1/ResortAPI",
                Token = token
            });
		}

		public Task<T> GetAsync<T>(int id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = resortUrl + "/api/v1/ResortAPI/" + id,
                Token = token
            });
		}

		public Task<T> UpdateAsync<T>(ResortUpdateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = resortUrl + "/api/v1/ResortAPI/" + dto.Id,
				Token = token
			});
		}
	}
}
