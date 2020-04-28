using MediatR;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RequestCommands.Patient.DeletePatient
{
    public class DeletePatientCommand : IRequest<ApiResponse>
    {
        public string PatientId { get; set; }
    }
}
