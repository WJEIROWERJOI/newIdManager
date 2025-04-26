using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers;
public class ApplicationRegisterDto
{
    
    [Required] public string UserName = string.Empty;
    [Required] [DataType(DataType.Password)] public string Password = string.Empty;
    [Required] [EmailAddress] public string Email = string.Empty;
    public UserRole Role = UserRole.User;
}