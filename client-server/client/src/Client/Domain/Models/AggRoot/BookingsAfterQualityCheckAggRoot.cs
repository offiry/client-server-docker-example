using Domain.Models.Base;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.AggRoot
{
    public class BookingsAfterQualityCheckAggRoot : AuditableEntity
    {
        public List<BookingAfterQualityCheckEntity> BookingsWithQualityCheck { get; set; }
    }
}
