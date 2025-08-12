using System.ComponentModel.DataAnnotations;

namespace Todo.Services.Abstraction.Services;
public interface IValidationService
{
    Task ValidateAndThrowAsync<T>(T instance);
}
