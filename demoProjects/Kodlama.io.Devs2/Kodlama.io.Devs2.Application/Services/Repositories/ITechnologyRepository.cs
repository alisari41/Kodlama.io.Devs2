using Core.Persistence.Repositories;
using Kodlama.io.Devs2.Domain.Entities;

namespace Kodlama.io.Devs2.Application.Services.Repositories;

public interface ITechnologyRepository : IAsyncRepository<Technology>, IRepository<Technology>
{

}
