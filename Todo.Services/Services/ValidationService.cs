using Autofac;

using FluentValidation;

using Todo.Services.Abstraction.Services;

public class ValidationService: IValidationService
{
    private readonly IComponentContext _context;

public ValidationService(IComponentContext context)
{
    _context = context;
}

public async Task ValidateAndThrowAsync<T>(T instance)
{
    var validator = _context.Resolve<IValidator<T>>();
    await validator.ValidateAndThrowAsync(instance);
}
}
