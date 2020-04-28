using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RequestCommands.Patient.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.DateOfBirth)
                .NotEmpty();
        }
    }
}
