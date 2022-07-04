using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class ConversionEntity : ValueObject
    {
        public string FromCoin { get; set; }
        public string ToCoin { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FromCoin;
            yield return ToCoin;
        }
    }
}
