using Core.Persistence.Repositories;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Persistence.Contexts;

namespace Kodlama.io.Devs2.Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}
