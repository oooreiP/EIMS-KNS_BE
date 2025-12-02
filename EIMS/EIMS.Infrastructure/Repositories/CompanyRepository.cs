using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;

namespace EIMS.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
        }

    }
}