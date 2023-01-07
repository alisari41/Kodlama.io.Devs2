using Kodlama.io.Devs2.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs2.Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task EmailCanNotBeDuplicatedWheRegistered(string email)
    {
        var result = await _userRepository.Query().Where(x => x.Email == email).AnyAsync();
        if (result) throw new Exception("Mail already Exists");
    }
}
