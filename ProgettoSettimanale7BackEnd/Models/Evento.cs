using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        public required string Titolo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public required string Luogo { get; set; }

        [ForeignKey("Artista")]
        public int ArtistaId { get; set; }

        public required Artista Artista { get; set; }
        public ICollection<Biglietto>? Biglietti { get; set; }
    }
}
