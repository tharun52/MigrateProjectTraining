using ShoppingApp.Models;

namespace ShoppingApp.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
        public Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(User user);
    }
}