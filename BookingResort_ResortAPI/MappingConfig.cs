using AutoMapper;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;

namespace BookingResort_ResortAPI
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Resort, ResortDTO>().ReverseMap();
			CreateMap<Resort, ResortCreateDTO>().ReverseMap();
			CreateMap<Resort, ResortUpdateDTO>().ReverseMap();

			CreateMap<ResortNumber, ResortNumberDTO>().ReverseMap();
			CreateMap<ResortNumber, ResortNumberCreateDTO>().ReverseMap();
			CreateMap<ResortNumber, ResortNumberUpdateDTO>().ReverseMap();
		}
	}
}
