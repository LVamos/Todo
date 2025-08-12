using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Requests;

namespace Todo.Services.Validation;
public class AddTodoListValidator: AbstractValidator<AddTodoListRequest>
{
    public AddTodoListValidator()
    {
        RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Name is required.")
    .MaximumLength(50)
    .WithMessage("Name cannot exceed 50 characters.");
    }
}
