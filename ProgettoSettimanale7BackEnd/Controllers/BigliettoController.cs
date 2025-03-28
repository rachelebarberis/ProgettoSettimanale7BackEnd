using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.DTOs.Biglietto;
using ProgettoSettimanale7BackEnd.Models;
using ProgettoSettimanale7BackEnd.Services;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _bigliettoService.GetBigliettoAsync();

                if (result == null || !result.Any())
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
    }
}
