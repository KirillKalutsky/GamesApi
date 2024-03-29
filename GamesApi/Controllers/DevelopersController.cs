﻿using GamesApi.Services;
using GamesApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("developers")]
    public class DevelopersController : BaseApiController
    {
        private readonly DeveloperService service;

        public DevelopersController(DeveloperService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers(
            [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")] int currentPage = 1,
            [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than or equal {1}")] int pageSize = 10)
        {
            var developers = await service
                .GetDevelopers(currentPage, pageSize);

            return ConvertApiResponse(developers);
        }

        [HttpGet("{developerId}")]
        public async Task<IActionResult> GetDeveloper([FromRoute] Guid developerId)
        {
            var developer = await service.GetDeveloper(developerId);

            return ConvertApiResponse(developer);
        }

        [HttpPost]
        public async Task<IActionResult> IncludeDeveloper([FromBody] StudioDeveloperDto dto)
        {
            var response = await service.IncludeDeveloper(dto);

            return ConvertApiResponse(response);
        }

        [HttpPatch("{developerId}")]
        public async Task<IActionResult> UpdateDeveloper([FromRoute] Guid developerId,
            [FromBody] UpdateStudioDeveloperDto dto)
        {
            var response = await service.UpdateDeveloper(developerId, dto);

            return ConvertApiResponse(response);
        }

        [HttpDelete("{developerId}")]
        public async Task<IActionResult> DeleteDeveloper([FromRoute] Guid developerId)
        {
            var response = await service.DeleteDeveloper(developerId);

            return ConvertApiResponse(response);
        }
    }
}
