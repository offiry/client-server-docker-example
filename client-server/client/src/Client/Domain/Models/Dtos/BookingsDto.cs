using Domain.Models.Base;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Dtos
{
    public class BookingsDto : AuditableEntity
    {
        public List<BookingEntity> Bookings { get; set; }
    }
}
