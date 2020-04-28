using MediatR;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RequestCommands.Patient.CreatePatient
{
    public class CreatePatientCommand : IRequest<ApiResponse>
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
