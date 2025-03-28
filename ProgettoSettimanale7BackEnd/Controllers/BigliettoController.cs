using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.DTOs.Biglietto;
using ProgettoSettimanale7BackEnd.Models;
using ProgettoSettimanale7BackEnd.Services;
using System.Security.Claims;

namespace ProgettoSettimanale7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigliettoController : ControllerBase
    {
        private readonly BigliettoService _bigliettoService;
        private readonly ApplicationDbContext _context;

        public BigliettoController(BigliettoService bigliettoService, ApplicationDbContext context)
        {
            _bigliettoService = bigliettoService;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([FromBody] CreateBigliettoRequestDto createBigliettoRequestDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(createBigliettoRequestDto.UserId);
                if (user == null)
                {
                    return BadRequest(new { message = "Utente non trovato." });
                }

                var evento = await _context.Eventi.FindAsync(createBigliettoRequestDto.EventoId);
                if (evento == null)
                {
                    return BadRequest(new { message = "Evento non trovato." });
                }

                var newBiglietto = new Biglietto()
                {
                    UserId = createBigliettoRequestDto.UserId,
                    EventoId = createBigliettoRequestDto.EventoId,
                    DataAcquisto = createBigliettoRequestDto.DataAcquisto
                };

                var result = await _bigliettoService.CreateBigliettoAsync(newBiglietto);

                return result
                    ? Ok(new { message = "Biglietto creato con successo!" })
                    : BadRequest(new { message = "Errore durante la creazione del biglietto." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                IQueryable<Biglietto> query = _context.Biglietti
                    .Include(b => b.ApplicationUser)
                    .Include(b => b.Evento);

                
                if (userRole == "User")
                {
                    query = query.Where(b => b.UserId == userId);
                }

                var result = await query.ToListAsync();

                if (!result.Any())
                {
                    return NotFound(new { message = "Nessun biglietto trovato." });
                }

                var responseDto = result.Select(b => new BigliettoDto()
                {
                    BigliettoId = b.BigliettoId,
                    UserId = b.UserId,
                    FirstName = b.ApplicationUser?.FirstName,
                    LastName = b.ApplicationUser?.LastName,
                    EventoId = b.EventoId,
                    Titolo = b.Evento?.Titolo,
                    DataAcquisto = b.DataAcquisto
                }).ToList();

                return Ok(new { message = "Biglietti trovati.", biglietti = responseDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBigliettoRequestDto updateBigliettoRequestDto)
        {
            try
            {
                var biglietto = await _context.Biglietti.FindAsync(id);
                if (biglietto == null)
                {
                    return NotFound(new { message = "Biglietto non trovato." });
                }

                var user = await _context.Users.FindAsync(updateBigliettoRequestDto.UserId);
                if (user == null)
                {
                    return BadRequest(new { message = "Utente non trovato." });
                }

                var evento = await _context.Eventi.FindAsync(updateBigliettoRequestDto.EventoId);
                if (evento == null)
                {
                    return BadRequest(new { message = "Evento non trovato." });
                }

                biglietto.UserId = updateBigliettoRequestDto.UserId;
                biglietto.EventoId = updateBigliettoRequestDto.EventoId;
                biglietto.DataAcquisto = updateBigliettoRequestDto.DataAcquisto;

                _context.Biglietti.Update(biglietto);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Biglietto aggiornato con successo!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var biglietto = await _context.Biglietti.FindAsync(id);
                if (biglietto == null)
                {
                    return NotFound(new { message = "Biglietto non trovato." });
                }

                _context.Biglietti.Remove(biglietto);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Biglietto eliminato con successo!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }
    }
}
