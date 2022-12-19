using Core.Persistence.Repositories;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using Kodlama.io.Devs2.Persistence.Contexts;

namespace Kodlama.io.Devs2.Persistence.Repositories;

public class TechnologyRepository : EfRepositoryBase<Technology, BaseDbContext>, ITechnologyRepository
{
    public TechnologyRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}
