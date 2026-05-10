using EfCoreDDD.Application.DTOs;
using EfCoreDDD.Application.Interface;
using EfCoreDDD.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDDD.API.Endpoints;

public class TaskListEndpoint : IEndpointGroup
{
    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet("/", () => "Test");

    }
    
}