using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services.Interfaces;

namespace HotelBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestsController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestsController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GuestDto>>> GetAll()
    {
        var guests = await _guestService.GetAllGuestsAsync();
        return Ok(guests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GuestDto>> GetById(Guid id)
    {
        var guest = await _guestService.GetGuestByIdAsync(id);
        if (guest == null) return NotFound();
        return Ok(guest);
    }

    [HttpPost]
    public async Task<ActionResult<GuestDto>> Create([FromBody] CreateGuestDto createGuestDto)
    {
        var guest = await _guestService.CreateGuestAsync(createGuestDto);
        return CreatedAtAction(nameof(GetById), new { id = guest.Id }, guest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGuestDto updateGuestDto)
    {
        await _guestService.UpdateGuestAsync(id, updateGuestDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _guestService.DeleteGuestAsync(id);
        return NoContent();
    }
}
