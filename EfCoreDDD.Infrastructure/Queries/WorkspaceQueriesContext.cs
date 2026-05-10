using EfCoreDDD.Application.Interface.Queries;
using EfCoreDDD.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Infrastructure.Queries;

public class WorkspaceQueriesContext(ApplicationDbContext db) : IWorkspaceQueries
{
    public async Task<IWorkspaceQueries.WorkspaceDto?> GetByIdAsync(int Id)
    {
        return await db.Workspaces
        .Where(w => w.Id == Id)
        .Select(w => new IWorkspaceQueries.WorkspaceDto(w.Id, w.Name, w.CreatedAt, w.UpdatedAt))
        .FirstOrDefaultAsync();
    }

    public async Task<IWorkspaceQueries.WorkspaceDetailDto?> GetDetailByIdAsync(int Id)
    {
        return await db.Workspaces
            .Where(w => w.Id == Id)
            .AsNoTracking().
            Select(w => new IWorkspaceQueries.WorkspaceDetailDto(
                w.Id,
                w.Name,
                w.CreatedAt,
                w.UpdatedAt,
                w.Lists.Select(tl => new IWorkspaceQueries.TaskListDTO(tl.Id, tl.WorkspaceId, tl.Title, tl.CreatedAt, tl.UpdatedAt)).ToList()
            )).FirstOrDefaultAsync();
    }

    public async Task<List<IWorkspaceQueries.WorkspaceDto>> GetListAsync()
    {
        return await db.Workspaces
            .AsNoTracking()
            .Select(w => new IWorkspaceQueries.WorkspaceDto(w.Id, w.Name, w.CreatedAt, w.UpdatedAt))
            .ToListAsync();
    }
    public async Task<List<IWorkspaceQueries.TaskListDTO>> GetTaskListsByWorkspaceAsync(int workspaceId)
    {
        return await db.TaskLists
            .AsNoTracking()
            .Where(tl => tl.WorkspaceId == workspaceId)
            .Select(tl => new IWorkspaceQueries.TaskListDTO(tl.Id, tl.WorkspaceId, tl.Title, tl.CreatedAt, tl.UpdatedAt))
            .ToListAsync();
    }

}
