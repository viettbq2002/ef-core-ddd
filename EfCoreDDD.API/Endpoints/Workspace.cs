using EfCoreDDD.API.Filters;
using EfCoreDDD.API.Helpers;
using EfCoreDDD.Application.DTOs;
using EfCoreDDD.Application.Interface;
using EfCoreDDD.Application.Interface.Queries;
using EfCoreDDD.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreDDD.API.Endpoints;

public class WorkspaceEndpoints : IEndpointGroup
{
    public static string? RoutePrefix => "/workspaces";
    public static void Map(RouteGroupBuilder groupBuilder)
    {

        groupBuilder.MapGet("", GetListWorkSpace);
        groupBuilder.MapPost("", CreateWorkspace)
            .AddEndpointFilter<ValidationFilter<CreateWorkspaceDTO>>()
            .ProducesValidationProblem();
        groupBuilder.MapPut("", UpdateWorkspace)
            .AddEndpointFilter<ValidationFilter<UpdateWorkspaceDTO>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
        groupBuilder.MapDelete("/{id:int}", DeleteWorkspace);
        groupBuilder.MapGet("/{id:int}", GetWorkSpaceById);
        groupBuilder.MapGet("/{id:int}/details", GetWorkSpaceDetailsById)
            .ProducesProblem(StatusCodes.Status404NotFound);
        groupBuilder.MapGet("/{workspaceId:int}/tasklists", GetTaskListsByWorkspaceId);
        groupBuilder.MapPost("/{workspaceId:int}/tasklists", AddTaskListToWorkspace);
    }

    [EndpointSummary("Create a workspace")]
    public static async Task<Results<Created<Workspace>, BadRequest<string>>> CreateWorkspace(IUnitOfWork uow, [FromBody] CreateWorkspaceDTO createWorkspaceDTO)
    {
        var workspaceRepo = uow.GetRepository<Workspace, int>();
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
    public static async Task<Results<Ok<IWorkspaceQueries.WorkspaceDto>, ProblemHttpResult>> GetWorkSpaceById(int id, IWorkspaceQueries workspaceQueries)
    {
        var workspace = await workspaceQueries.GetByIdAsync(id);
        if (workspace == null)
            return ProblemHelper.WorkspaceNotFound(id);
        return TypedResults.Ok(workspace);
    }
    [EndpointSummary("Get workspace details by ID")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IWorkspaceQueries.WorkspaceDetailDto))]
    public static async Task<Results<Ok<IWorkspaceQueries.WorkspaceDetailDto>, ProblemHttpResult>> GetWorkSpaceDetailsById(int id, IWorkspaceQueries workspaceQueries)
    {
        var workspace = await workspaceQueries.GetDetailByIdAsync(id);
        if (workspace == null)
            return ProblemHelper.WorkspaceNotFound(id);
        return TypedResults.Ok(workspace);
    }
    [EndpointSummary("Delete workspace")]
    public static async Task<Results<Ok<string>, ProblemHttpResult>> DeleteWorkspace(int id, IUnitOfWork uow)
    {
        var workspaceRepo = uow.GetRepository<Workspace, int>();
        var workspace = await workspaceRepo.TryFindAsync(id);
        if (workspace == null)
            return ProblemHelper.WorkspaceNotFound(id);
        workspaceRepo.Delete(workspace);
        await uow.SaveChangesAsync();
        return TypedResults.Ok($"Workspace with id {id} deleted successfully");
    }
    [EndpointSummary("Update workspace")]
    public static async Task<Results<Ok<string>, ProblemHttpResult>> UpdateWorkspace([FromBody] UpdateWorkspaceDTO updateWorkspaceDTO, IUnitOfWork uow)
    {
        //TODO: Add validation for updateWorkspaceDTO
        var workspaceRepo = uow.GetRepository<Workspace, int>();
        var workspace = await workspaceRepo.TryFindAsync(updateWorkspaceDTO.Id);
        if (workspace == null)
            return ProblemHelper.WorkspaceNotFound(updateWorkspaceDTO.Id);
        workspace.UpdateWorkspace(updateWorkspaceDTO.Name);
        await uow.SaveChangesAsync();
        return TypedResults.Ok($"Workspace with id {updateWorkspaceDTO.Id} updated successfully");
    }
    [EndpointSummary("Get task lists by workspace ID")]
    public static async Task<Ok<List<IWorkspaceQueries.TaskListDTO>>> GetTaskListsByWorkspaceId(int workspaceId, IWorkspaceQueries workspaceQueries)
    {
        var taskLists = await workspaceQueries.GetTaskListsByWorkspaceAsync(workspaceId);
        return TypedResults.Ok(taskLists);
    }
    [EndpointSummary("Add a task list to a workspace")]
    public static async Task<Results<Created<TaskList>, ProblemHttpResult>> AddTaskListToWorkspace(int workspaceId, [FromBody] CreateTaskListDTO createTaskListDTO, IUnitOfWork uow)
    {
        var taskListWorkspace = uow.GetRepository<TaskList, int>();
        var workspaceRepo = uow.GetRepository<Workspace, int>();
        var isWorkspaceExist = await workspaceRepo.AnyAsync(workspaceId);
        if (!isWorkspaceExist)
            return ProblemHelper.WorkspaceNotFound(workspaceId);
        var createdTaskList = new TaskList(createTaskListDTO.Title, workspaceId);
        taskListWorkspace.Add(createdTaskList);
        await uow.SaveChangesAsync();
        return TypedResults.Created($"/workspaces/{workspaceId}/tasklists/{createdTaskList.Id}", createdTaskList);
    }

}
