using Core.Persistence.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Services.Repositories
{
    public interface IProgrammingLanguageRepository : IAsyncRepository<ProgrammingLanguage>, IRepository<ProgrammingLanguage>
    {
    }
}
