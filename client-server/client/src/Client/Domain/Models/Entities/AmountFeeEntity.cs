using Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Entities
{
    public class AmountFeeEntity : AuditableEntity
    {
        public long FromAmount { get; set; }
        public long ToAmount { get; set; }
        public float AmountFee { get; set; }
    }
}
