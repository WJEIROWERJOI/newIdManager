using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.ApplicationUsers
{
    public class ApplicationLoginDto
    {
        [Required(ErrorMessage = "아이디를 입력하세요")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "비번을 입력하세요")]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }
}
