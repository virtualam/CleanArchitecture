using AutoMapper;
using Domain.Entities.Resources;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.MappingProfiles
{
    public class PatientToPatientDtoProfile : Profile
    {
        public PatientToPatientDtoProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dest=> dest.Gender, 
                           opt => opt.MapFrom(src => src.Gender.HasValue ? src.Gender.ToString() : string.Empty));
        }
    }
}
