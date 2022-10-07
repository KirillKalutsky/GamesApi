using GamesApi.Services;
using Microsoft.AspNetCore.Mvc;
using GamesApi.Models;
using GamesApi.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : BaseApiController
    {
        private readonly GameService service;

        public GamesController(GameService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGamesByGenre(GameGenre? genre,
            [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")] int currentPage = 1,
            [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")] int pageSize = 10)
        {
            ApiResponse response;
            if (genre == null)
                response = await service.GetGamesAsync(currentPage, pageSize);
            else
                response = await service
                .GetGamesByGenreAsync(genre.Value, currentPage, pageSize);

            return ConvertApiResponse(response);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            var game = await service.FindGameById(gameId);

            return ConvertApiResponse(game);
        }

        [HttpPost]
        public async Task<IActionResult> IncludeGame([FromBody] GameDto dto)
        {
            var response = await service.IncludeGame(dto);

            return ConvertApiResponse(response);
        }

        [HttpPatch("{gameId}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid gameId,
            [FromBody] UpdateGameDto dto)
        {
            var response = await service.UpdateGame(gameId, dto);

            return ConvertApiResponse(response);
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            var response = await service.DeleteGame(gameId);

            return ConvertApiResponse(response);
        }
    }
}
