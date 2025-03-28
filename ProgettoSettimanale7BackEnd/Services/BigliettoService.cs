using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.Models;

namespace ProgettoSettimanale7BackEnd.Services
{
    public class BigliettoService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BigliettoService> _logger;

        public BigliettoService(ApplicationDbContext context, ILogger<BigliettoService> logger)
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
                _logger.LogError(ex, "Errore durante il salvataggio nel database");
                return false;
            }
        }

        public async Task<bool> CreateBigliettoAsync(Biglietto biglietto)
        {
            try
            {
                _context.Biglietti.Add(biglietto);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione del biglietto");
                return false;
            }
        }

        public async Task<List<Biglietto>?> GetBigliettoAsync()
        {
            try
            {
                return await _context.Biglietti
                    .Include(b => b.ApplicationUser)
                    .Include(b => b.Evento)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nel recupero dei biglietti");
                return null;
            }
        }
    }
}
