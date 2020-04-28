using Domain.Entities.Resources;
using Domain.Interfaces;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> FindAll(IPaginationFilter paginationFilter);
        Task<IEnumerable<PatientDto>> FindByCondition(Expression<Func<Patient, bool>> expression, IPaginationFilter paginationFilter);
        Task Create(Patient entity);
        Task Update(string patientId, Patient entity);
        Task Delete(string patientId);
    }
}
