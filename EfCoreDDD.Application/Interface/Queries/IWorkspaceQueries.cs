using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface.Queries
{
    public interface IWorkspaceQueries
    {
        public record WorkspaceDto(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
        Task<WorkspaceDto?> GetByIdAsync(int Id);
        Task<List<WorkspaceDto>> GetListAsync();
    }
}
