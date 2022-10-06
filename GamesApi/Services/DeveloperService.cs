using GamesApi.Models.Dto;
using AutoMapper;
using GamesApi.DB;
using GamesApi.Models;

namespace GamesApi.Services
{
    public class DeveloperService:ApiService
    {
        private readonly IDeveloperRepository repository;
        private readonly IMapper mapper;
        public DeveloperService(IDeveloperRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ApiResponse> GetDevelopers(int currentPage, int pageSize)
        {
            var developers = await repository
                .GetAsync(currentPage, pageSize);

            return CreateSuccessResponse(developers);
        }

        public async Task<ApiResponse> GetDeveloper(Guid developerId)
        {
            var developer = await repository.GetAsync(developerId);

            if (developer == null)
                return CreateNotFoundResponse("");

            return CreateSuccessResponse(developer);
        }

        public async Task<ApiResponse> IncludeDeveloper(StudioDeveloperDto dto)
        {
            if (dto == null || dto.IsAnyPropertiesNullOrEmpty())
                return CreateBadRequest("");

            var developer = mapper.Map<StudioDeveloper>(dto);

            /*if (!ModelState.IsValid)
                return BadRequest();*/

            await repository.InsertAsync(developer);

            return CreateSuccessResponse();
        }

        public async Task<ApiResponse> UpdateDeveloper(Guid developerId, GameDto dto)
        {
            if (dto == null || dto.IsAllPropertiesNullOrEmpty())
                return CreateBadRequest("");

            var developer = await repository.GetAsync(developerId);

            if (developer == null)
                return CreateNotFoundResponse("");

            var updateDeveloper = new StudioDeveloper(developerId);
            mapper.Map(dto, updateDeveloper);

            /*if (!ModelState.IsValid)
                return BadRequest();*/

            await repository.UpdateAsync(updateDeveloper);

            return CreateSuccessResponse();
        }

        public async Task<ApiResponse> DeleteDeveloper(Guid developerId)
        {
            var developer = await repository.GetAsync(developerId);

            if (developer == null)
                return CreateNotFoundResponse("");

            await repository.DeleteAsync(developerId);

            return CreateSuccessResponse();
        }
    }
}
