﻿using Core.Security.Entities;

namespace Kodlama.io.Devs2.Application.Services.UserServices;

public interface IUserServices
{
    public Task<User?> GetByEmail(string email);
    public Task<User> GetById(int id);
    public Task<User> Update(User user);
}
