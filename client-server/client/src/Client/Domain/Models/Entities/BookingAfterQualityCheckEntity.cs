using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class BookingAfterQualityCheckEntity : ValueObject
    {
        public string Reference { get; set; }
        public long? Amount { get; set; }
        public long AmountWithFees { get; set; }
        public long? AmountReceived { get; set; }
        public string QualityCheck { get; set; }
        public bool OverPayment { get; set; }
        public bool UnderPayment { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Reference;
            yield return Amount;
            yield return AmountWithFees;
            yield return AmountReceived;
            yield return QualityCheck;
            yield return OverPayment;
            yield return UnderPayment;
        }
    }
}
