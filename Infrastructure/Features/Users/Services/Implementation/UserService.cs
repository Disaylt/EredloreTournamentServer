using System.Net;
using Application.Features.Users.Services.Abstraction;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Features.Users.Services.Implementation;

public sealed class UserService(UserManager<UserEntity> userManager)
    : IUserService
{
    public async Task<UserEntity> CreateAsync(string email, string userName, string password)
    {
        var user = new UserEntity
        {
            Email = email,
            UserName = userName
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded is false)
        {
            throw CreateBadRegisterException(result);
        }

        return user;
    }

    public async Task DeleteAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id)
                ?? throw CreateUserNotFoundException();

        await userManager.DeleteAsync(user);
    }

    public async Task<UserEntity> FindByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email)
                ?? throw new CoreRequestException()
                    .SetStatusCode(HttpStatusCode.NotFound)
                    .AddMessages(["Пользователь с такой почтой не найден."]);
    }

    public async Task<bool> VerifyPasswordAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null) return false;

        return await userManager.CheckPasswordAsync(user, password);
    }

    private static CoreRequestException CreateBadRegisterException(IdentityResult identityResult)
    {
        var error = new CoreRequestException();

        foreach(var message in identityResult.Errors)
        {
            error.AddKeyMessages(message.Code, [message.Description]);
        }

        return error;
    }

    private static CoreRequestException CreateUserNotFoundException()
    {
        return new CoreRequestException()
                .AddMessages(["Пользователь не найден."]);
    }
}
