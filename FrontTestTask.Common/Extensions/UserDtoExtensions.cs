using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Models;

namespace FrontTestTask.Common.Extensions;

internal static class UserDtoExtensions
{
    public static UserProfile ToProfile(this UserDto user) => new(user.Id, user.Email, user.Name, user.Role, user.Avatar);
}
