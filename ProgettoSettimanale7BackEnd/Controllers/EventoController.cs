using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale7BackEnd.Data;

using ProgettoSettimanale7BackEnd.DTOs.Evento;
using ProgettoSettimanale7BackEnd.DTOs.Artista;
using ProgettoSettimanale7BackEnd.Models;
using ProgettoSettimanale7BackEnd.Services;



namespace ProgettoSettimanale7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {

        private readonly EventoService _eventoService;
        private readonly ILogger<EventoController> _logger;

        private readonly ApplicationDbContext _context;

        public EventoController(EventoService eventoService, ILogger<EventoController> logger, ApplicationDbContext context)
        {
            _eventoService = eventoService;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventoRequestDto createEventoRequestDto)
        {
            try
            {

                var artista = await _context.Artisti.FindAsync(createEventoRequestDto.ArtistaId);
                if (artista == null)
                {
                    return BadRequest(new { message = "Artista non trovato." });
                }

                var newEvento = new Evento()
                {
                    Titolo = createEventoRequestDto.Titolo,
                    Data = createEventoRequestDto.Data,
                    Luogo = createEventoRequestDto.Luogo,
                    ArtistaId = createEventoRequestDto.ArtistaId,
                    Artista = artista
                };

                var result = await _eventoService.CreateEventoAsync(newEvento);

                return result
                    ? Ok(new { message = "Evento creato con successo!" })
                    : BadRequest(new { message = "Si è verificato un errore durante la creazione dell'evento." });
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
             
                var result = await _eventoService.GetEventoAsync();

             
                if (result == null || !result.Any())
                {
                    return NotFound(new { message = "Nessun evento trovato." });
                }

          
                var responseDto = result.Select(r => new EventoDto()
                {
                    EventoId = r.EventoId,
                    Titolo = r.Titolo,
                    Data = r.Data,
                    Luogo = r.Luogo,
                    Artista = new ArtistaDto
                    {
                        ArtistaId = r.Artista?.ArtistaId ?? 0,
                        Nome = r.Artista?.Nome
                    }
                }).ToList();

                return Ok(new { message = "Eventi trovati.", eventi = responseDto });
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventoRequestDto updateEventoRequestDto)
        {
            try
            {
                var evento = await _context.Eventi.FindAsync(id);
                if (evento == null)
                {
                    return NotFound(new { message = "Evento non trovato." });
                }

                // Verifica se l'Artista esiste
                var artista = await _context.Artisti.FindAsync(updateEventoRequestDto.ArtistaId);
                if (artista == null)
                {
                    return BadRequest(new { message = "Artista associato non trovato." });
                }

                // Aggiorna i campi
                evento.Titolo = updateEventoRequestDto.Titolo;
                evento.Data = updateEventoRequestDto.Data;
                evento.Luogo = updateEventoRequestDto.Luogo;
                evento.ArtistaId = updateEventoRequestDto.ArtistaId;
                evento.Artista = artista;

                _context.Eventi.Update(evento);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Evento aggiornato con successo!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _context.Eventi.FindAsync(id);
                if (evento == null)
                {
                    return NotFound(new { message = "Evento non trovato." });
                }

                _context.Eventi.Remove(evento);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Evento eliminato con successo!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno", error = ex.Message });
            }
        }

    }
}


