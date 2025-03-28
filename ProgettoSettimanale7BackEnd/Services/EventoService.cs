using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.Models;

namespace ProgettoSettimanale7BackEnd.Services
{
    public class EventoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EventoService> _logger;

        public EventoService(ApplicationDbContext context, ILogger<EventoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateEventoAsync(Evento evento)
        {
            try
            {
                _context.Eventi.Add(evento);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }


        public async Task<List<Evento>?> GetEventoAsync()
        {
            try
            {
                var eventi = await _context.Eventi.Include(a => a.Artista).ToListAsync();
                return eventi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}

