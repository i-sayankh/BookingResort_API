using BookingResort_Web.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingResort_Web.Models.VM
{
    public class ResortNumberUpdateVM
    {
        public ResortNumberUpdateVM()
        {
            ResortNumber = new ResortNumberUpdateDTO();
        }
        public ResortNumberUpdateDTO ResortNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ResortList { get; set; }
    }
}
