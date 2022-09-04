namespace TodoMinimalApi.Features.Authorization.Dtos
{
    public record RegisterUserDto(string Email, string Password, string ConfirmPassword);

}
