namespace TaskManager.Application.Models.Auth
{
    public class EmailConfirmCode
    {
        public string Email { get; set; } = null!;
        public int Code { get; set; }

        public static string GetCacheKey(string email)
        {
            return $"confirm_{email}";
        }
    }
}