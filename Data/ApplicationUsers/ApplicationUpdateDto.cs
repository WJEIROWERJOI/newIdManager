using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers;

public class ApplicationUpdateDto
{
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "Username �ʿ��մϴ�.")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email �ʿ��մϴ�.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    //[Required]
    //public string OldPassword { get; set; } = string.Empty;
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    public UserRole Role { get; set; } = UserRole.User;
}