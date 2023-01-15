using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kodlama.io.Devs2.Application.Services.AuthService;

public class AuthManager : IAuthService
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly TokenOptions _tokenOptions;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _tokenHelper = tokenHelper;
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        var addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task<AccessToken> CreatedAccessToken(User user)
    {
        IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id,
                                                                                                            include: x => x.Include(x => x.OperationClaim));

        IList<OperationClaim> operationClaims = userOperationClaims.Items.Select(x => new OperationClaim
        {
            Id = x.OperationClaimId,
            Name = x.OperationClaim.Name
        }).ToList();

        AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
        return accessToken;
    }

    public async Task<RefreshToken> CreatedRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return await Task.FromResult(refreshToken);
    }

    public async Task DeleteOldRefreshToken(int userId)
    {
        IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(x =>
                                                    x.UserId == userId &&
                                                    x.Revoked == null && x.Expires >= DateTime.UtcNow &&
                                                    x.Created.AddDays(_tokenOptions.RefreshTokenTTL) <=
                                                    DateTime.UtcNow)
                                            ).Items;

        foreach (RefreshToken refreshToken in refreshTokens) await _refreshTokenRepository.DeleteAsync(refreshToken);
    }

    //public async Task SendAuthenticatorCode(User user)
    //{
    //    if (user.AuthenticatorType is AuthenticatorType.Email) await SendAuthenticatorCodeWithEmail(user);
    //}

    //private async Task SendAuthenticatorCodeWithEmail(User user)
    //{
    //    EmailAuthenticator emailAuthenticator = await _ema
    //}


}
