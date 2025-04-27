using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers
{
    public class ApplicationViewDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}
