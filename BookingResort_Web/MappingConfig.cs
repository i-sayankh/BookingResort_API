using AutoMapper;
using BookingResort_Web.Models.DTO;

namespace BookingResort_Web
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<ResortDTO, ResortCreateDTO>().ReverseMap();
			CreateMap<ResortDTO, ResortUpdateDTO>().ReverseMap();
			
			CreateMap<ResortNumberDTO, ResortNumberCreateDTO>().ReverseMap();
			CreateMap<ResortNumberDTO, ResortNumberUpdateDTO>().ReverseMap();
		}
	}
}
