using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.Models
{
    public class Artista
    {
        [Key]
        public int ArtistaId { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Genere { get; set; }
        [Required]
        public string Biografia { get; set; }
        public ICollection<Evento>? Eventi { get; set; }
    }
}
