using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.Posts
{
    public class PostCreateDto
    {
        [Required(ErrorMessage = "Title missing")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "content missing")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author missing")]
        public string Author { get; set; } = string.Empty;
       }
}
