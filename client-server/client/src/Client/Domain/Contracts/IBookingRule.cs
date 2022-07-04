using Domain.Contracts.Base;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IBookingRule : IRuleEngine
    {
        BookingAfterQualityCheckEntity GetBookingAfterQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck);
    }
}
