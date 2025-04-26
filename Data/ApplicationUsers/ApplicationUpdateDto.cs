using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers;

public class ApplicationUpdateDto
{
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "Username 필요합니다.")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email 필요합니다.")]
    public string Email { get; set; } = string.Empty;
    //[Required]
    //public string OldPassword { get; set; } = string.Empty;
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    public UserRole Role { get; set; } = UserRole.User;
}