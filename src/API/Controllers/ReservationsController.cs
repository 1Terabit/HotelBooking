using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services.Interfaces;

namespace HotelBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(reservations);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetActive()
    {
        var reservations = await _reservationService.GetActiveReservationsAsync();
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetById(Guid id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> Create([FromBody] CreateReservationDto createReservationDto)
    {
        var reservation = await _reservationService.CreateReservationAsync(createReservationDto);
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateReservationDto updateReservationDto)
    {
        await _reservationService.UpdateReservationAsync(id, updateReservationDto);
        return NoContent();
    }
}
