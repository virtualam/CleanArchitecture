using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    public class ServiceAPIController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
