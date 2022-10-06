using GamesApi.Services;
using GamesApi;
using Microsoft.AspNetCore.Mvc;
using GamesApi.DB.Repositories;
using GamesApi.Models;
using AutoMapper;
using GamesApi.Models.Dto;
using GamesApi.DB;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : BaseApiController
    {
        private readonly GameService service;

        public GamesController(GameService gameService)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames(int currentPage, int pageSize)
        {
            var response = await service.GetGamesAsync(currentPage, pageSize);

            return ConvertApiResponse(response);
        }

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetGamesByGenre([FromRoute] GameGenre genre, int currentPage, int pageSize)
        {
            var games = await service
                .GetGamesByGenreAsync(genre, currentPage, pageSize);

            return ConvertApiResponse(games);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            var game = await service.FindGameById(gameId);

            return ConvertApiResponse(game);
        }

        [HttpGet("developer/{developerId}")]
        public async Task<IActionResult> GetDeveloperGames([FromRoute] Guid developerId,
            int currentPage, int pageSize)
        {
            var response = await service.GetGamesByDeveloperId(developerId, currentPage, pageSize);

            return ConvertApiResponse(response);
        }

        [HttpGet("developer/{developerId}/genre/{genre}")]
        public async Task<IActionResult> GetDeveloperGamesByGenre([FromRoute] Guid developerId,
            [FromRoute] GameGenre genre, int currentPage, int pageSize)
        {
            var response = await service.GetGamesByDeveloperIdAndGenre(developerId, genre, currentPage, pageSize);

            return ConvertApiResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> IncludeGame([FromBody] GameDto dto)
        {
            var response = await service.IncludeGame(dto);

            return ConvertApiResponse(response);
        }

        [HttpPatch("{gameId}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid gameId,
            [FromBody] GameDto dto)
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
