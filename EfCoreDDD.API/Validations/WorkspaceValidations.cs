using EfCoreDDD.Application.DTOs;
using FluentValidation;

namespace EfCoreDDD.API.Validations;

public class CreateWorkspaceValidations : AbstractValidator<CreateWorkspaceDTO>
{
    public CreateWorkspaceValidations()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}
public class UpdateWorkspaceValidations : AbstractValidator<UpdateWorkspaceDTO>
{
    public UpdateWorkspaceValidations()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}