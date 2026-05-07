using EfCoreDDD.Application.Interface.Queries;
using EfCoreDDD.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Infrastructure.Queries
{
    public class WorkspaceQueriesContext(ApplicationDbContext db) : IWorkspaceQueries
    {
        public async Task<IWorkspaceQueries.WorkspaceDto?> GetByIdAsync(int Id)
        {
            return await db.Workspaces
            .Where(w => w.Id == Id)
            .Select(w => new IWorkspaceQueries.WorkspaceDto(w.Id, w.Name, w.CreatedAt, w.UpdatedAt))
            .FirstOrDefaultAsync();
        }

        public Task<List<IWorkspaceQueries.WorkspaceDto>> GetListAsync()
        {
            return db.Workspaces.Select(w => new IWorkspaceQueries.WorkspaceDto(w.Id, w.Name, w.CreatedAt, w.UpdatedAt)).ToListAsync();
        }
    }
}
