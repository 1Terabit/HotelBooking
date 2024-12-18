using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services.Interfaces;

namespace HotelBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll()
    {
        var hotels = await _hotelService.GetAllHotelsAsync();
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> GetById(Guid id)
    {
        var hotel = await _hotelService.GetHotelByIdAsync(id);
        if (hotel == null) return NotFound();
        return Ok(hotel);
    }

    [HttpPost]
    public async Task<ActionResult<HotelDto>> Create([FromBody] CreateHotelDto createHotelDto)
    {
        var hotel = await _hotelService.CreateHotelAsync(createHotelDto);
        return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHotelDto updateHotelDto)
    {
        await _hotelService.UpdateHotelAsync(id, updateHotelDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _hotelService.DeleteHotelAsync(id);
        return NoContent();
    }
}
