using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Artista
{
    public class CreateArtistaRequestDto
    {
        [Required]
        public required string Nome { get; set; }
        [Required]
        public required string Genere { get; set; }
        [Required]
        public required string Biografia { get; set; }
    }
}
