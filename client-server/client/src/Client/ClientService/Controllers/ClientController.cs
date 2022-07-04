using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Handlers;
using Domain.Models.AggRoot;
using Domain.Models.Dtos;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IHttpRequests _httpRequests;

        public ClientController(IHttpRequests httpRequests)
        {
            _httpRequests = httpRequests ?? throw new ArgumentNullException(nameof(httpRequests));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Client Service Up...");
        }

        /**
         * https://localhost:44363/api/client/payments_with_quality_check
         */
        [HttpGet("payments_with_quality_check")]
        [ProducesResponseType(typeof(BookingsAfterQualityCheckAggRoot), StatusCodes.Status200OK)]
        public async Task<BookingsAfterQualityCheckAggRoot> GetAllBookingsWithQualityCheck
            ([FromServices] IMediator mediator) => await mediator.Send(new GetAllBookingsRequest());
    }
}
