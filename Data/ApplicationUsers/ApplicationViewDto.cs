namespace newIdManager.Data.ApplicationUsers
{
    public class ApplicationViewDto
    {
        public string Id { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
