using VibeHome.Domain.Entities;

namespace VibeHome.UI
{
    public static class AuthState
    {
        public static User? CurrentUser { get; set; }
        public static void Logout() => CurrentUser = null;
    }
} 