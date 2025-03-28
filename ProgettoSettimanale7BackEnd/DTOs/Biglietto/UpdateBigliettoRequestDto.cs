using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Biglietto
{
    public class UpdateBigliettoRequestDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int EventoId { get; set; }

        [Required]
        public DateTime DataAcquisto { get; set; }
    }
}
