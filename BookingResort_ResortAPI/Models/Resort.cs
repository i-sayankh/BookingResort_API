﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingResort_ResortAPI.Models
{
	public class Resort
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Details { get; set; }
		public double Rate { get; set; }
		public int Sqft { get; set; }
		public int Occupancy { get; set; }
		public string ImageURL { get; set; }
		public string Amenity { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set;}
	}
}
