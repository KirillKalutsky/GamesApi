using GamesApi;
using Microsoft.AspNetCore.Mvc;
using GamesApi.DB.Repositories;
using GamesApi.Models;
using AutoMapper;
using GamesApi.Models.Dto;

namespace GamesApi.Controllers
{
    [Route("{controller}")]
    public class GamesController : ControllerBase
    {
        private readonly GameRepository gameRepository;
        private readonly DevelopersRepository developersRepository;
        private readonly IMapper mapper;

        public GamesController(IMapper mapper, GameRepository gameRepository,
            DevelopersRepository developersRepository)
        {
            this.mapper = mapper;
            this.gameRepository = gameRepository;
            this.developersRepository = developersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames(int currentPage, int pageSize)
        {
            var games = await gameRepository
                .GetAllAsync(currentPage, pageSize);

            return Ok(games);
        }

        [HttpGet("genre/{genre}")]
        public async Task<IActionResult> GetGamesByGenre([FromRoute] GameGenre genre, int currentPage, int pageSize)
        {
            var games = await gameRepository
                .GetGamesByGenreAsync(genre, currentPage, pageSize);

            return Ok(games);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            var game = await gameRepository.ReadAsync(gameId);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        [HttpGet("developer/{developerId}")]
        public async Task<IActionResult> GetDeveloperGames([FromRoute] Guid developerId,
            int currentPage, int pageSize)
        {
            var developer = await developersRepository.ReadAsync(developerId);

            if (developer == null)
                return NotFound();

            var games = await gameRepository.
                GetGamesByDeveloperAsync(developerId, currentPage, pageSize);

            return Ok(games);
        }

        [HttpGet("developer/{developerId}/genre/{genre}")]
        public async Task<IActionResult> GetDeveloperGamesByGenre([FromRoute] Guid developerId,
            [FromRoute] GameGenre genre, int currentPage, int pageSize)
        {
            var developer = await developersRepository.ReadAsync(developerId);

            if (developer == null)
                return NotFound();

            var games = await gameRepository.
               GetGamesByGenreAndDeveloperAsync(genre, developerId, currentPage, pageSize);

            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> IncludeGame([FromBody] GameDto dto)
        {
            if (dto == null || dto.IsAnyPropertiesNullOrEmpty())
                return BadRequest();

            var game = mapper.Map<Game>(dto);

            if (!ModelState.IsValid)
                return BadRequest();

            await gameRepository.CreateAsync(game);

            return NoContent();
        }

        [HttpPatch("{gameId}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid gameId,
            [FromBody] GameDto dto)
        {
            if (dto == null || dto.IsAllPropertiesNullOrEmpty())
                return BadRequest();

            var game = await gameRepository.ReadAsync(gameId);

            if (game == null)
                return NotFound();

            var newGame = new Game(gameId);
            mapper.Map(dto,newGame);

            if (!ModelState.IsValid)
                return BadRequest();

            await gameRepository.UpdateAsync(newGame);

            return NoContent();
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            var game = await gameRepository.ReadAsync(gameId);

            if (game == null)
                return NotFound();

            await gameRepository.DeleteAsync(gameId);

            return NoContent();
        }
    }
}
