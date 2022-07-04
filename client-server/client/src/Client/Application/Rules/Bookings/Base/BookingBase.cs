using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Rules.Bookings.Base
{
    public abstract class BookingBase : IBookingRule
    {
        protected abstract BookingAfterQualityCheckEntity DoBookingQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck);
        private Regex _validEmailRegex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                                + "@"
                                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");

        public BookingAfterQualityCheckEntity GetBookingAfterQualityCheck(BookingEntity booking, BookingAfterQualityCheckEntity bookingAfterQualityCheck)
         => DoBookingQualityCheck(booking, bookingAfterQualityCheck);

        protected bool IsEmailValid(string email) => _validEmailRegex.Match(email).Success;
    }
}
