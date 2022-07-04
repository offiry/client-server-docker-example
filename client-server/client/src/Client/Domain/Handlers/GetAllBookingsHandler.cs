using Domain.Contracts;
using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Models.Dtos;
using AutoMapper;
using Domain.Models.AggRoot;

namespace Domain.Handlers
{
    public class GetAllBookingsRequest : IRequest<BookingsAfterQualityCheckAggRoot>
    {
    }

    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsRequest, BookingsAfterQualityCheckAggRoot>
    {
        private readonly IHttpRequests _httpRequests;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IBookingRule[] _bookingRules;

        public GetAllBookingsHandler(IHttpRequests httpRequests, IConfiguration configuration, IMapper mapper, IBookingRule[] bookingRules)
        {
            _httpRequests = httpRequests ?? throw new ArgumentNullException(nameof(httpRequests));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookingRules = bookingRules ?? throw new ArgumentNullException(nameof(bookingRules));
        }

        public async Task<BookingsAfterQualityCheckAggRoot> Handle(GetAllBookingsRequest request, CancellationToken cancellationToken)
        {
            var bookings = await _httpRequests.Get<BookingsDto>(_configuration.GetSection("ServerApi").GetSection("GetAllBookings").Value);

            BookingsAfterQualityCheckAggRoot bookingsAfterQualityCheckDto = new BookingsAfterQualityCheckAggRoot
            {
                BookingsWithQualityCheck = new List<BookingAfterQualityCheckEntity>()
            };

            foreach (var booking in bookings.Bookings)
            {
                BookingAfterQualityCheckEntity bookingAfterQualityCheck = new BookingAfterQualityCheckEntity();

                foreach (var rule in _bookingRules)
                {
                    bookingAfterQualityCheck = rule.GetBookingAfterQualityCheck(booking, bookingAfterQualityCheck);
                }

                bookingsAfterQualityCheckDto.BookingsWithQualityCheck.Add(bookingAfterQualityCheck);
            }

            return bookingsAfterQualityCheckDto;
        }
    }
}
