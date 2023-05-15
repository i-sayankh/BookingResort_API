using AutoMapper;
using BookingResort_ResortAPI.Data;
using BookingResort_ResortAPI.Models;
using BookingResort_ResortAPI.Models.DTO;
using BookingResort_ResortAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace BookingResort_ResortAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/ResortNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ResortNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IResortNumberRepository _dbResortNumber;
        private readonly IResortRepository _dbResort;
        private readonly IMapper _mapper;
        public ResortNumberAPIController(IResortNumberRepository dbResortNumber, IMapper mapper, IResortRepository dbResort)
        {
            _dbResortNumber = dbResortNumber;
            _mapper = mapper;
            _response = new();
            _dbResort = dbResort;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
