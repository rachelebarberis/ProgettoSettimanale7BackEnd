using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Biglietto
{
    public class CreateBigliettoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
