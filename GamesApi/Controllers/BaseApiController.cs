using GamesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers
{
    public class BaseApiController: ControllerBase
    {
        protected virtual ActionResult ConvertApiResponse(ApiResponse response)
        {
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();

            return StatusCode(response.StatusCode, response);
        }
    }
}
