﻿using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Kodlama.io.Devs2.Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim>, IRepository<OperationClaim>
{
}
