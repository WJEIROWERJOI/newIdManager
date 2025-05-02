using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers;

public class ApplicationUpdateDto
{
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "UserName missing")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email missing")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string NewPassword { get; set; } = string.Empty;
    public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    public UserRole Role { get; set; } = UserRole.User;
}