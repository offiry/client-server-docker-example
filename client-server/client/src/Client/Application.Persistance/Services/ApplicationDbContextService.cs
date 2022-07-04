using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persistance.Services
{
    public class ApplicationDbContextService : IApplicationDbContext
    {
        private readonly ApplicationDbContext _applicationDbContext;
    }
}
