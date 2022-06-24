using AutoMapper;
using GamesApi.DB.Repositories;
using GamesApi.Models;
using GamesApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers
{
    [Route("{controller}")]
    public class DevelopersController : Controller
    {
        private readonly DevelopersRepository developersRepository;
        private readonly IMapper mapper;

        public DevelopersController(IMapper mapper, DevelopersRepository gameRepository)
        {
            this.mapper = mapper;
            this.developersRepository = gameRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetGames(int currentPage, int pageSize)
        {
            var developers = await developersRepository
                .GetAllAsync(currentPage, pageSize);

            return Ok(developers);
        }

        [HttpGet("{developerId}")]
        public async Task<IActionResult> GetGame([FromRoute] Guid developerId)
        {
            var developer = await developersRepository.ReadAsync(developerId);

            if (developer == null)
                return NotFound();

            return Ok(developer);
        }

        [HttpPost]
        public async Task<IActionResult> IncludeGame([FromBody] StudioDeveloperDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest();

            var developer = mapper.Map<StudioDeveloper>(dto);

            if (!ModelState.IsValid)
                return BadRequest();

            await developersRepository.CreateAsync(developer);

            return NoContent();
        }

        [HttpPatch("{developerId}")]
        public async Task<IActionResult> UpdateGame([FromRoute] Guid developerId,
            [FromBody] GameDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest();

            var developer = await developersRepository.ReadAsync(developerId);

            if (developer == null)
                return NotFound();

            var updateDeveloper = new StudioDeveloper(developerId);
            mapper.Map(dto, updateDeveloper);

            await developersRepository.UpdateAsync(updateDeveloper);

            return NoContent();
        }

        [HttpDelete("{developerId}")]
        public async Task<IActionResult> DeleteGame([FromRoute] Guid developerId)
        {
            var developer = await developersRepository.ReadAsync(developerId);

            if (developer == null)
                return NotFound();

            await developersRepository.DeleteAsync(developerId);

            return NoContent();
        }
    }
}
