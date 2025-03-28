using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Evento
{
    public class UpdateEventoRequestDto
    {
        [Required]
        public required string Titolo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public required string Luogo { get; set; }

        [Required]
        public required int ArtistaId { get; set; }

        [Required]
        public required string Nome { get; set; }
    }
}
