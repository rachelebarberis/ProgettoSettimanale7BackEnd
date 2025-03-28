using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimanale7BackEnd.DTOs.Artista
{
    public class UpdateArtistaResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }

}
