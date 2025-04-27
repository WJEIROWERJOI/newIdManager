using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.Posts
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [Required(ErrorMessage ="missing")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "missing")]
        public string Author { get; set; } = string.Empty;
    }
}
