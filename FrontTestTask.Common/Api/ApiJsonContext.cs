using System.Text.Json.Serialization;
using FrontTestTask.Common.Api.Models.Dto;
using FrontTestTask.Common.Api.Models.Request;
using FrontTestTask.Common.Api.Models.Response;

namespace FrontTestTask.Common.Api;

/// <summary>
/// Source-generated контекст сериализации: без рефлексии, имена свойств в snake_case.
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
[JsonSerializable(typeof(LoginRequest))]
[JsonSerializable(typeof(RegisterRequest))]
[JsonSerializable(typeof(RefreshTokenRequest))]
[JsonSerializable(typeof(LogoutRequest))]
[JsonSerializable(typeof(LoginResponse))]
[JsonSerializable(typeof(RefreshTokenResponse))]
[JsonSerializable(typeof(UserDto))]
[JsonSerializable(typeof(ApiErrorResponse))]
internal sealed partial class ApiJsonContext : JsonSerializerContext;
