using Core.Security.Entities;
using Kodlama.io.Devs2.Application.Services.Repositories;

namespace Kodlama.io.Devs2.Application.Services.UserServices;

public class UserManager : IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email);
        return user;
    }

    public async Task<User> GetById(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id);
        return user;
    }

    public async Task<User> Update(User user)
    {
        var updateUser = await _userRepository.UpdateAsync(user);
        return updateUser;
    }
}
