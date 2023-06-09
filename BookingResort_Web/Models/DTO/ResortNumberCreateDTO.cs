﻿using System.ComponentModel.DataAnnotations;

namespace BookingResort_Web.Models.DTO
{
	public class ResortNumberCreateDTO
	{
		[Required]
		public int ResortNo { get; set; }
		[Required]
		public int ResortId { get; set; }
		public string SpecialDetails { get; set; }
	}
}
