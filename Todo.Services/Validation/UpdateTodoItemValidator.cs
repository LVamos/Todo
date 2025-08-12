using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Requests;

namespace Todo.Services.Validation;
public class UpdateTodoItemValidator: AbstractValidator<UpdateTodoItemRequest>
{
    public UpdateTodoItemValidator()
    {
        RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Title is required.")
    .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");
    }
}
