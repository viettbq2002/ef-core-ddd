using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface.Queries;

public interface ITaskListQueries
{
    public record TaskListDTO(int Id, int workspaceId, string Title, DateTime CreatedAt, DateTime UpdatedAt);
    public record TaskListDetailDTO (int Id, string Title, DateTime CreatedAt, DateTime UpdatedAt, List<TaskItemDTO> TaskItems);
    public record TaskItemDTO (int Id, string Title, bool IsCompleted, DueDate DueDate);
}
