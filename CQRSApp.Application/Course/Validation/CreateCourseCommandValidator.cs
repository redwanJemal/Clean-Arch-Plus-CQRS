using CQRSApp.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Validations
{
    public class CreateCourseCommandValidator: AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(c => c.DepartmentId).NotEmpty();
            RuleFor(c => c.CreditHour).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
