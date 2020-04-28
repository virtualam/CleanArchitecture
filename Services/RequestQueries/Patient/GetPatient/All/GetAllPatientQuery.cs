using MediatR;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RequestQueries.Patient.GetPatient.All
{
    public class GetAllPatientQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
