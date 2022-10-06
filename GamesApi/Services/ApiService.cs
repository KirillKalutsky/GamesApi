using GamesApi.Models;

namespace GamesApi.Services
{
    public abstract class ApiService
    {
        protected ApiResponse CreateSuccessResponse(object data) =>
            new ApiResponse { Data = data, StatusCode = 200 };

        protected ApiResponse CreateSuccessResponse() =>
            new ApiResponse { StatusCode = 204 };

        protected ApiResponse CreateNotFoundResponse(string error) =>
            new ApiResponse { StatusCode = 404, ErrorMessage = error };

        protected ApiResponse CreateBadRequest(string error) =>
            new ApiResponse { StatusCode = 400, ErrorMessage = error };
    }
}
