using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Biglietto
{
    public class UpdateBigliettoResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
