using Application.Contracts;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private Dictionary<ConversionEntity, float> _conversionRates = new Dictionary<ConversionEntity, float>
        {
            { new ConversionEntity { FromCoin = "USD", ToCoin = "USD"}, 1f },
            { new ConversionEntity { FromCoin = "EUR", ToCoin = "USD"}, 1.5f },
            { new ConversionEntity { FromCoin = "CAD", ToCoin = "USD"}, 2f },
        };

        public CurrencyConverterService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public float GetConversionRate(string fromCoin, string toCoin)
        {
            return _conversionRates[new ConversionEntity { FromCoin = fromCoin.ToUpper(), ToCoin = toCoin.ToUpper() }];
        }
    }
}
