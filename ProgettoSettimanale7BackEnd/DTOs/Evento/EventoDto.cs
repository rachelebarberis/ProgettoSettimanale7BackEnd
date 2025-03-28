using ProgettoSettimanale7BackEnd.DTOs.Artista;

namespace ProgettoSettimanale7BackEnd.DTOs.Evento
{
    public class EventoDto
    {
        public int EventoId { get; set; }
        public required string Titolo { get; set; }
        public DateTime Data { get; set; }
        public required string Luogo { get; set; }
        public required ArtistaDto Artista { get; set; }
    }

}
