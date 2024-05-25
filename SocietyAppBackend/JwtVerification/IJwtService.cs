namespace SocietyAppBackend.JwtVerification
{
    public interface IJwtService
    {
        int GetUserIdFromToken(string token);

    }
}
