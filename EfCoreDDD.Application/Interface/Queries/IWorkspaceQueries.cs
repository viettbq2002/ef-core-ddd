using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface.Queries
{
    public interface IWorkspaceQueries
    {
        public record WorkspaceDto(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
        public record TaskListDTO(int Id, int workspaceId, string Title, DateTime CreatedAt, DateTime UpdatedAt);
        public record WorkspaceDetailDto(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt, List<TaskListDTO> TaskLists);
        Task<WorkspaceDto?> GetByIdAsync(int Id);
        Task<WorkspaceDetailDto?> GetDetailByIdAsync(int Id);
        Task<List<WorkspaceDto>> GetListAsync();
        Task<List<TaskListDTO>> GetTaskListsByWorkspaceAsync(int workspaceId);
        
    }
}
