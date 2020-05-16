using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Services.DTOs;
using Services.Interfaces;
using Services.PipelineBehaviors;
using Services.RepoServices;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Services
{
    public static class Extensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IPatientRepository, Infrastructure.Dapper.SqlServer.PatientRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddTransient<IErrorModel, ErrorModel>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IMvcBuilder AddControllerServices(this IMvcBuilder mvcBuilder)
        {
            //mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            return mvcBuilder;
        }
    }
}
