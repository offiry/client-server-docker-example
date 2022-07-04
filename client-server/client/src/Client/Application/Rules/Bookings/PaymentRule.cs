using Application.Rules.Bookings.Base;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rules.Bookings
{
    public class PaymentRule : BookingBase
    {
        private readonly ICurrencyConverterService _currencyConverter;

        public PaymentRule(ICurrencyConverterService currencyConverter)
            : base()
        {
            _currencyConverter = currencyConverter ?? throw new ArgumentNullException(nameof(currencyConverter));
        }

        protected override BookingAfterQualityCheckEntity DoBookingQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck)
        {
            if (booking.Amount is null
               || booking.Amount <= 0)
            {
                bookingAfterQualityCheck.OverPayment = false;
                bookingAfterQualityCheck.UnderPayment = false;
            }
            else
            {
                bookingAfterQualityCheck.OverPayment =
                    (booking.Amount_Received * _currencyConverter.GetConversionRate(booking.Currency_From, "USD")) > booking.Amount;
                bookingAfterQualityCheck.UnderPayment =
                    (booking.Amount_Received * _currencyConverter.GetConversionRate(booking.Currency_From, "USD")) < booking.Amount;
            }
           

            return bookingAfterQualityCheck;
        }
    }
}
