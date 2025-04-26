using System.ComponentModel.DataAnnotations;

namespace newIdManager.Data.Posts
{
    public class PostEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsPublished { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int Views { get; set; } = 0;
        public int CommentCount { get; set; } = 0;

    }
}
