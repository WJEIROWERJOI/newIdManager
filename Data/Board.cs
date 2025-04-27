using System.ComponentModel.DataAnnotations;
using newIdManager.Data.ApplicationUsers;

namespace newIdManager.Data
{
    public class Board
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        //public List<Board> Boards { get; set; } = new();
        //public string Icon { get; set; } = "";
        //public bool RequiresAuth { get; set; } = false;
        //public UserRole? RequiredRole { get; set; } = null;
        public string Img { get; set; } = string.Empty;
    }
}
