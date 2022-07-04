using Application.Rules.Bookings.Base;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rules.Bookings
{
    public class AmountWithFeesRule : BookingBase
    {
        private readonly IAmountFeeService _amountFeeService;
        private readonly ICurrencyConverterService _currencyConverter;

        public AmountWithFeesRule(IAmountFeeService amountFeeService, ICurrencyConverterService currencyConverter)
            : base()
        {
            _amountFeeService = amountFeeService ?? throw new ArgumentNullException(nameof(amountFeeService));
            _currencyConverter = currencyConverter ?? throw new ArgumentNullException(nameof(currencyConverter));
        }

        protected override BookingAfterQualityCheckEntity DoBookingQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck)
        {
            if (booking.Amount is null
                || booking.Amount <= 0)
            {
                bookingAfterQualityCheck.Amount = 0;
                bookingAfterQualityCheck.AmountReceived = 0;
                bookingAfterQualityCheck.AmountWithFees = 0;
            }
            else
            {
                bookingAfterQualityCheck.Amount = booking.Amount;
                bookingAfterQualityCheck.AmountReceived = (long)((float)booking.Amount_Received * _currencyConverter.GetConversionRate(booking.Currency_From, "USD"));
                bookingAfterQualityCheck.AmountWithFees = (long)((float)booking.Amount + (float)booking.Amount * _amountFeeService.GetFeeByAmount(booking.Amount));
            }

            return bookingAfterQualityCheck;
        }
    }
}
