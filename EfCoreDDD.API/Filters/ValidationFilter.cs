using FluentValidation;

namespace EfCoreDDD.API.Filters;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.Arguments.FirstOrDefault(x => x is T) as T;

        if (argToValidate is not null)
        {
            var validationResult = await validator.ValidateAsync(argToValidate);
            if (!validationResult.IsValid)
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
        }

        return await next(context);
    }
}

