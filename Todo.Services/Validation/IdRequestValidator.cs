using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Contracts.Contracts.Requests;

namespace Todo.Services.Validation;
public class IdRequestValidator: AbstractValidator<IdRequest>
{
    public IdRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");
    }
}
