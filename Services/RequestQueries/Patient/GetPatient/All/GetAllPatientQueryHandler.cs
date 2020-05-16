using Infrastructure.Utility;
using MediatR;
using Services.Interfaces;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.RequestQueries.Patient.GetPatient.All
{
    public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, ApiResponse>
    {
        private readonly IPatientService _patientService;

        public GetAllPatientQueryHandler(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        public async Task<ApiResponse> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var paginationFilter = new PaginationFilter();
            if (request != null && request.PageNumber > 0 && request.PageSize > 0)
            {
                paginationFilter = new PaginationFilter(request.PageNumber, request.PageSize);
            }
            var result = await _patientService.FindAll(paginationFilter);
            return new ApiResponse(result, result.Any());
        }
    }
}
