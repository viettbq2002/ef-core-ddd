using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EfCoreDDD.API.Endpoints
{
    public class TaskListEndpoint : IEndpointGroup
    {
        public static void Map(RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/", () => "Test");

        }
    }
}
