using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ICurrencyConverterService
    {
        float GetConversionRate(string fromCoin, string toCoin);
    }
}
