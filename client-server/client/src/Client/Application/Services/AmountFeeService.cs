using Application.Contracts;
using Domain.Contracts;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class AmountFeeService : IAmountFeeService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private List<AmountFeeEntity> _amountFeesRate = new List<AmountFeeEntity>
        {
            { new AmountFeeEntity { FromAmount = 0L, ToAmount = 1000L, AmountFee = 0.05f } },
            { new AmountFeeEntity { FromAmount = 1001L, ToAmount = 10000L, AmountFee = 0.03f } },
            { new AmountFeeEntity { FromAmount = 10001L, ToAmount = Int64.MaxValue, AmountFee = 0.02f } },
        };

        public AmountFeeService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public float GetFeeByAmount(long? amount)
            => _amountFeesRate.Where(a => amount > a.FromAmount && amount <= a.ToAmount)
                .FirstOrDefault().AmountFee;
    }
}
