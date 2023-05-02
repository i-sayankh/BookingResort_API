using BookingResort_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingResort_Web.Models.VM
{
    public class ResortNumberDeleteVM
    {
        public ResortNumberDeleteVM()
        {
            ResortNumber = new ResortNumberDTO();
        }
        public ResortNumberDTO ResortNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ResortList { get; set; }
    }
}
