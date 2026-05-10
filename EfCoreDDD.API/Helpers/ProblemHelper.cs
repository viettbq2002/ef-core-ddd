using Microsoft.AspNetCore.Http.HttpResults;

namespace EfCoreDDD.API.Helpers
{
    public static class ProblemHelper
    {
        public static ProblemHttpResult WorkspaceNotFound(int workspaceId) => 
            ObjectNotFound($"Workspace with id {workspaceId}");
        private static ProblemHttpResult ObjectNotFound(string objName) =>
            TypedResults.Problem(detail: $"{objName} not found", statusCode: StatusCodes.Status404NotFound);
        public static ProblemHttpResult BadRequest(string message) =>
            TypedResults.Problem(detail: message, statusCode: StatusCodes.Status400BadRequest);

    }
}
