using Core.Persistence.Repositories;
using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Persistence.Contexts;

namespace Kodlama.io.Devs2.Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, BaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}
