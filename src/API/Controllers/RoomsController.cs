using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services.Interfaces;

namespace HotelBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetById(Guid id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAvailable(
        [FromQuery] DateTime checkIn,
        [FromQuery] DateTime checkOut)
    {
        var rooms = await _roomService.GetAvailableRoomsAsync(checkIn, checkOut);
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Create([FromBody] CreateRoomDto createRoomDto)
    {
        var room = await _roomService.CreateRoomAsync(createRoomDto);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomDto updateRoomDto)
    {
        await _roomService.UpdateRoomAsync(id, updateRoomDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}
