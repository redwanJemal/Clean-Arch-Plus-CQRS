using CQRSApp.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Validations
{
    public class CreateDepartmentCommandValidator: AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(d => d.Name).NotEmpty();
        }
    }
}
