using EfCoreDDD.Application.DTOs;
using EfCoreDDD.Application.Interface;
using EfCoreDDD.Application.Interface.Queries;
using EfCoreDDD.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDDD.API.Endpoints
{
    public class WorkspaceEndpoints : IEndpointGroup
    {

        public static void Map(RouteGroupBuilder groupBuilder)
        {

            groupBuilder.MapGet("", GetListWorkSpace);
            groupBuilder.MapPost("", CreateWorkspace);
            groupBuilder.MapPut("/update", UpdateWorkspace);
            groupBuilder.MapDelete("/{id:int}", DeleteWorkspace);
            groupBuilder.MapGet("/{id:int}", GetWorkSpaceById);
        }

        [EndpointSummary("Create a workspace")]
        public static async Task<Results<Created<Workspace>, BadRequest<string>>> CreateWorkspace(IUnitOfWork uow, [FromBody] CreateWorkspaceDTO createWorkspaceDTO)
        {
            var workspaceRepo = uow.GetRepository<Workspace, int>();

            if (workspaceRepo == null) return TypedResults.BadRequest($"Failed to load {nameof(workspaceRepo)}");

            var workspace = new Workspace(createWorkspaceDTO.Name);
            workspaceRepo.Add(workspace);
            int result = await uow.SaveChangesAsync();
            return TypedResults.Created($"/workspaces/{workspace.Id}", workspace);

        }
        [EndpointSummary("Get list of workspaces")]
        public static async Task<Ok<List<IWorkspaceQueries.WorkspaceDto>>> GetListWorkSpace(IWorkspaceQueries workspaceQueries)
        {
            var list = await workspaceQueries.GetListAsync();
            return TypedResults.Ok(list);
        }
        [EndpointSummary("Get workspace by ID")]
        public static async Task<Results<Ok<IWorkspaceQueries.WorkspaceDto>, NotFound<string>>> GetWorkSpaceById(int id, IWorkspaceQueries workspaceQueries)
        {
            var workspace = await workspaceQueries.GetByIdAsync(id);
            if (workspace == null) return TypedResults.NotFound($"Workspace with id {id} not found");
            return TypedResults.Ok(workspace);
        }
        [EndpointSummary("Delete workspace")]
        public static async Task<Results<Ok<string>, BadRequest<string>, NotFound<string>>> DeleteWorkspace(int id, IUnitOfWork uow)
        {
            var workspaceRepo = uow.GetRepository<Workspace, int>();
            if (workspaceRepo == null) return TypedResults.BadRequest($"Failed to load {nameof(workspaceRepo)}");
            var workspace = await workspaceRepo.TryFindAsync(id);
            if (workspace == null) return TypedResults.NotFound($"Workspace with id {id} not found");
            workspaceRepo.Delete(workspace);
            await uow.SaveChangesAsync();
            return TypedResults.Ok($"Workspace with id {id} deleted successfully");
        }
        [EndpointSummary("Update workspace")]
        public static async Task<Results<Ok<string>, BadRequest<string>, NotFound<string>>> UpdateWorkspace([FromBody] UpdateWorkspaceDTO updateWorkspaceDTO, IUnitOfWork uow)
        {
            //TODO: Add validation for updateWorkspaceDTO
            var workspaceRepo = uow.GetRepository<Workspace, int>();
            if (workspaceRepo == null) return TypedResults.BadRequest($"Failed to load {nameof(workspaceRepo)}");
            var workspace = await workspaceRepo.TryFindAsync(updateWorkspaceDTO.Id);
            if (workspace == null) return TypedResults.NotFound($"Workspace with id {updateWorkspaceDTO.Id} not found");
            workspace.UpdateWorkspace(updateWorkspaceDTO.Name);
            await uow.SaveChangesAsync();
            return TypedResults.Ok($"Workspace with id {updateWorkspaceDTO.Id} updated successfully");
        }
    }
}
