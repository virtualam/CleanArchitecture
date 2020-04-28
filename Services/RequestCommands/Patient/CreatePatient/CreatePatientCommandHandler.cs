using Domain.Enums;
using MediatR;
using Services.Interfaces;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.RequestCommands.Patient.CreatePatient
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, ApiResponse>
    {
        private readonly IPatientService _patientService;

        public CreatePatientCommandHandler(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        public Task<ApiResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            Gender? gender = null;
            if (!string.IsNullOrEmpty(request.Gender))
            {
                gender = (Gender)Enum.Parse(typeof(Gender), request.Gender, true);
            }

            _patientService.Create(
                new Domain.Entities.Resources.Patient
                {
                    Name = request.Name,
                    DateOfBirth = request.DateOfBirth,
                    Gender = gender
                });

            return Task.FromResult(new ApiResponse());
        }
    }
}
