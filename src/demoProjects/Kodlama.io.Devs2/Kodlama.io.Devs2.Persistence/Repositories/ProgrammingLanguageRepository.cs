using Core.Persistence.Repositories;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using Kodlama.io.Devs2.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Persistence.Repositories
{
    public class ProgrammingLanguageRepository : EfRepositoryBase<ProgrammingLanguage, BaseDbContext>, IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(BaseDbContext context) : base(context)
        {

        }
    }
}
