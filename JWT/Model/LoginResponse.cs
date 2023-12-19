namespace JWT.Model
{
    public class LoginResponse
    {
        public string name { get; set; }
        public string roleId { get; set; } = "ABC456";
        public string email { get; set; }
        public string mobile { get; set; }
        public string designation { get; set; }
        public string level { get; set; }
        public string permission { get; set; }
        public string officeCode { get; set; } = "ABC123";
    }
    public class JwtTokenResponse
    {
        public string? Token { get; set; }
        public bool IsRefreshToken { get; set; }
    }
    public class UserRequest
    {
        public string roleId { get; set; }
        public string officeCode { get; set; }
    }
    public class JWTSettings
    {
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public int ExpireInMinutes { get; set; } = 0;
    }
}
