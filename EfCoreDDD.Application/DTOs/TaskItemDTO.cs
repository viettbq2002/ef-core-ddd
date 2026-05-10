using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.DTOs;

public record CreateTaskItemDTO (string Name, string Description, int TaskListId);

