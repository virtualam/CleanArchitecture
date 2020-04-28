using MediatR;
using Services.Interfaces;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.RequestCommands.Patient.DeletePatient
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, ApiResponse>
    {
        private readonly IPatientService _patientService;

        public DeletePatientCommandHandler(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        public Task<ApiResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            _patientService.Delete(request.PatientId);

            return Task.FromResult(new ApiResponse());
        }
    }
}
