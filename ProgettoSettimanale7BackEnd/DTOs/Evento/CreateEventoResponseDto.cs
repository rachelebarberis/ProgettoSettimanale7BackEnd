using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Evento
{
    public class CreateEventoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
