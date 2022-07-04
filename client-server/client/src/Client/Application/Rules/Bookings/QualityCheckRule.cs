using Application.Rules.Bookings.Base;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rules.Bookings
{
    public class QualityCheckRule : BookingBase
    {
        private Dictionary<string, int> _bookingReferences = new Dictionary<string, int>();
        private const string _invalidEmail = "InvalidEmail";
        private const string _duplicatedPayment = "DuplicatedPayment";
        private const string _amountThreshold = "AmountThreshold";
        private readonly ICurrencyConverterService _currencyConverter;

        public QualityCheckRule(ICurrencyConverterService currencyConverter)
            : base()
        {
            _currencyConverter = currencyConverter ?? throw new ArgumentNullException(nameof(currencyConverter));
        }

        protected override BookingAfterQualityCheckEntity DoBookingQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck)
        {
            StringBuilder qualityCheckResult = new StringBuilder();
            bookingAfterQualityCheck.QualityCheck = qualityCheckResult
                .Append(base.IsEmailValid(booking.Email) ? string.Empty : _invalidEmail)
                .Append(",")
                .Append(_bookingReferences.ContainsKey(booking.Reference) ? _duplicatedPayment : string.Empty)
                .Append(",")
                .Append(booking.Amount_Received * _currencyConverter.GetConversionRate(booking.Currency_From, "USD") > 1000000 ? _amountThreshold : string.Empty)
                .ToString();

            bookingAfterQualityCheck.Reference = booking.Reference;

            var addNewBooking = _bookingReferences.TryAdd(booking.Reference, 1);

            if (!addNewBooking)
            {
                _bookingReferences[booking.Reference]++;
            }

            return bookingAfterQualityCheck;
        }
    }
}
