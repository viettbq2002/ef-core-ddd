using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Application.DTOs;

public record CreateWorkspaceDTO(string Name);
public record UpdateWorkspaceDTO(int Id, string Name);

