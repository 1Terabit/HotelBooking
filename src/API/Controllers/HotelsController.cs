using Microsoft.AspNetCore.Mvc;
using HotelBooking.Application.Services;
using HotelBooking.Application.DTOs;

namespace HotelBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateHotelDto createHotelDto)
        {
            // Implementation
            return Ok();
        }
    }
}
