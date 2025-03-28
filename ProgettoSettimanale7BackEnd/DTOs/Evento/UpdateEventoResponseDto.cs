using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Evento
{
    public class UpdateEventoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
