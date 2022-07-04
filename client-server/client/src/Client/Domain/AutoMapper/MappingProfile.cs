using AutoMapper;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AutoMapper
{
    partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingEntity, BookingAfterQualityCheckEntity>();
        }
    }
}
