
namespace TodoMinimalApi.Features.Authorization.Dtos
{
    public class AuthorizeResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
