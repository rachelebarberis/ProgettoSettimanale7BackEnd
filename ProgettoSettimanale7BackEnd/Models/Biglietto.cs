using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.Models
{
    public class Biglietto
    {
        [Key]
        public int BigliettoId { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Required]
        public DateTime DataAcquisto { get; set; }

        public  Evento? Evento { get; set; }
        public  ApplicationUser? ApplicationUser { get; set; }
    }
}
