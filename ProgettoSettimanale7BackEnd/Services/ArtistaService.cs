using Microsoft.EntityFrameworkCore;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.Models;

namespace ProgettoSettimanale7BackEnd.Services
{
    public class ArtistaService
    {
   
            private readonly ApplicationDbContext _context;
            private readonly ILogger<ArtistaService> _logger;

            public ArtistaService(ApplicationDbContext context, ILogger<ArtistaService> logger)
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

            public async Task<bool> CreateArtistaAsync(Artista artista)
            {
                try
                {
                    _context.Artisti.Add(artista);
                    return await SaveAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return false;
                }
            }

            public async Task<List<Artista>?> GetArtistiAsync()
            {
                try
                {
                    var artisti = await _context.Artisti.ToListAsync();
                    return artisti;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return null;
                }
            }
        }
}
