using ProgettoSettimanale7BackEnd.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Biglietto
{
    public class BigliettoDto
    {
        public int BigliettoId { get; set; }

        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int EventoId { get; set; }
        public string? Titolo { get; set; }

        [Required]
        public DateTime DataAcquisto { get; set; }

    }
}