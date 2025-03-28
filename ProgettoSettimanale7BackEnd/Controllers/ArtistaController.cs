﻿using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoSettimanale7BackEnd.Data;
using ProgettoSettimanale7BackEnd.DTOs.Artista;
using ProgettoSettimanale7BackEnd.DTOs.Evento;
using ProgettoSettimanale7BackEnd.Models;
using ProgettoSettimanale7BackEnd.Services;

namespace ProgettoSettimanale7BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistaController : ControllerBase
    {
      
        
            private readonly ArtistaService _artistaService;
            private readonly ILogger<ArtistaController> _logger;

            private readonly ApplicationDbContext _context;

            public ArtistaController(ArtistaService artistaService, ILogger<ArtistaController> logger, ApplicationDbContext context)
            {
                _artistaService = artistaService;
                _logger = logger;
                _context = context;
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateArtistaRequestDto createArtistaRequestDto)
            {
                try
                {
                    var newArtista = new Artista()
                    {
                        Nome = createArtistaRequestDto.Nome,
                        Genere = createArtistaRequestDto.Genere,
                        Biografia = createArtistaRequestDto.Biografia,

                    };

                    var result = await _artistaService.CreateArtistaAsync(newArtista);

                    return result ? Ok(new CreateArtistaResponseDto() { Message = "Artist created!" }) : BadRequest(new CreateArtistaResponseDto() { Message = "Something went wrong!" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);

                }
            }



            [HttpGet]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> GetAll()
            {
                var result = await _artistaService.GetArtistiAsync();

                List<ArtistaDto> responseDto = result.Select(r => new ArtistaDto()
                {
                    ArtistaId = r.ArtistaId,
                    Nome = r.Nome,
                    Genere = r.Genere,
                    Biografia = r.Biografia,

                }).ToList();


                _logger.LogInformation($"Requesting artists info: {JsonSerializer.Serialize(responseDto, new JsonSerializerOptions() { WriteIndented = true })}");

                return result != null ? Ok(new { message = "Artist found", artist = responseDto }) : BadRequest(new { message = "Something went wrong" });
            }


        }
    }



