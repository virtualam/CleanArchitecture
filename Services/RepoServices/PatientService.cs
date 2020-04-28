using AutoMapper;
using Domain.Entities.Resources;
using Domain.Interfaces;
using Services.DTOs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.RepoServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            this._mapper = mapper;
        }
        public Task Create(Patient entity)
        {
            return _patientRepository.Create(entity);
        }

        public Task Delete(string patientId)
        {
            return _patientRepository.Delete(patientId);
        }

        public async Task<IEnumerable<PatientDto>> FindAll(IPaginationFilter paginationFilter)
        {
            IEnumerable<Patient> patients = await _patientRepository.FindAll(paginationFilter);
            var response = _mapper.Map<IEnumerable<PatientDto>>(patients);
            return response;
        }

        public async Task<IEnumerable<PatientDto>> FindByCondition(Expression<Func<Patient, bool>> expression, IPaginationFilter paginationFilter)
        {
            IEnumerable<Patient> patients = await _patientRepository.FindByCondition(expression, paginationFilter);
            var response = _mapper.Map<IEnumerable<PatientDto>>(patients);
            return response;
        }

        public Task Update(string patientId, Patient entity)
        {
            return _patientRepository.Update(patientId, entity);
        }
    }
}
