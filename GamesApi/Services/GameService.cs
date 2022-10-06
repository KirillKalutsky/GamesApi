using GamesApi.Models.Dto;
using GamesApi.Models;
using GamesApi.DB;
using AutoMapper;

namespace GamesApi.Services
{
    public class GameService: ApiService
    {
        private readonly IGameRepository gameRepository;
        private readonly IDeveloperRepository developersRepository;
        private readonly IMapper mapper;


        public GameService(IGameRepository gameRepository, IDeveloperRepository developerRepository,
            IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.developersRepository = developerRepository;
            this.mapper = mapper;

        }

        public async Task<ApiResponse> GetGamesAsync(int currentPage, int pageSize)
        {
            var games = await gameRepository
                .GetAsync(currentPage, pageSize);

            return CreateSuccessResponse(games);
        }

        public async Task<ApiResponse> GetGamesByGenreAsync(GameGenre genre, int currentPage, int pageSize)
        {
            var games = await gameRepository
                .GetGamesByGenreAsync(genre, currentPage, pageSize);

            return CreateSuccessResponse(games);
        }

        public async Task<ApiResponse> FindGameById( Guid gameId)
        {
            var game = await gameRepository.GetAsync(gameId);

            if (game == null)
                return CreateNotFoundResponse($"Не найдена игра с идентификатором {gameId}");

            return CreateSuccessResponse(game);
        }

        public async Task<ApiResponse> GetGamesByDeveloperId(Guid developerId,
            int currentPage, int pageSize)
        {
            var developer = await developersRepository.GetAsync(developerId);

            if (developer == null)
                return CreateNotFoundResponse($"{developerId}");

            var games = await gameRepository.
                GetGamesByDeveloperAsync(developerId, currentPage, pageSize);

            return CreateSuccessResponse(games);
        }

        public async Task<ApiResponse> GetGamesByDeveloperIdAndGenre(Guid developerId,
           GameGenre genre, int currentPage, int pageSize)
        {
            var developer = await developersRepository.GetAsync(developerId);

            if (developer == null)
                return CreateNotFoundResponse($"{developerId}");

            var games = await gameRepository.
               GetGamesByGenreAndDeveloperAsync(genre, developerId, currentPage, pageSize);

            return CreateSuccessResponse(games);
        }

        public async Task<ApiResponse> IncludeGame(GameDto dto)
        {
            if (dto == null || dto.IsAnyPropertiesNullOrEmpty())
                return CreateBadRequest("");

            var game = mapper.Map<Game>(dto);

            /*if (!ModelState.IsValid)
                return BadRequest();*/

            await gameRepository.InsertAsync(game);

            return CreateSuccessResponse();
        }

        public async Task<ApiResponse> UpdateGame(Guid gameId, GameDto dto)
        {
            if (dto == null || dto.IsAllPropertiesNullOrEmpty())
                return CreateBadRequest("");

            var game = await gameRepository.GetAsync(gameId);

            if (game == null)
                return CreateNotFoundResponse("");

            var newGame = new Game(gameId);
            mapper.Map(dto, newGame);

            /*if (!ModelState.IsValid)
                return BadRequest();*/

            await gameRepository.UpdateAsync(newGame);

            return CreateSuccessResponse();
        }

        public async Task<ApiResponse> DeleteGame(Guid gameId)
        {
            var game = await gameRepository.GetAsync(gameId);

            if (game == null)
                return CreateNotFoundResponse("");

            await gameRepository.DeleteAsync(gameId);

            return CreateSuccessResponse();
        }
    }
}
