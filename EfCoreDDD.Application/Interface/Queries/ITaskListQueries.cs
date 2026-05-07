using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.Interface.Queries
{
    public interface ITaskListQueries
    {
        public record TaskListDto(Guid Id, string Name, string Description, DateTime CreatedAt, DateTime UpdatedAt);

    }
}
