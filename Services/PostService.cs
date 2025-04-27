using Microsoft.EntityFrameworkCore;
using newIdManager.Data;
using newIdManager.Data.Posts;

namespace newIdManager.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        //C
        public async Task AddPostAsync(PostCreateDto dto)
        {
            var pst = new PostEntity
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            await _context.PostEntities.AddAsync(pst);
            await _context.SaveChangesAsync();
            return;
        }
        public async Task ViewPlus(int id)
        {
            var pst = await _context.PostEntities.FindAsync(id);
            if (pst is not null) { pst.Views++; }
            await _context.SaveChangesAsync();
        }
        public async Task CountCommentAsync(int id)
        {
            var pst = await _context.PostEntities.FindAsync(id);
            if (pst is not null)
            {
                pst.CommentCount = await _context.CommentEntities.Where(u => u.PostId == id).CountAsync();
                await _context.SaveChangesAsync();
            }
        }

        //R
        public async Task<List<PostEntity>> GetPagePostsAsync(int Page, int amount)
        {
            return await _context.PostEntities
                .OrderByDescending(p => p.CreatedAt)
                .Skip((Page - 1) * amount)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<List<PostEntity>> SearchPostsAsync(string str, string target, int Page, int amount)
        {
            var posts = new List<PostEntity>();
            switch (target)
            {
                case "Title":
                    posts = await _context.PostEntities
                        .Where(p => p.Title.Contains(str))
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip((Page - 1) * amount)
                        .Take(amount)
                        .ToListAsync();
                    break;
                case "Content":
                    posts = await _context.PostEntities
                        .Where(p => p.Content.Contains(str))
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip((Page - 1) * amount)
                        .Take(amount)
                        .ToListAsync();
                    break;
                case "Author":
                    posts = await _context.PostEntities
                        .Where(p => p.Author == str)
                        .OrderByDescending(p => p.CreatedAt)
                        .Skip((Page - 1) * amount)
                        .Take(amount)
                        .ToListAsync();
                    break;
                default: break;
            }
            return posts;
        }
        public async Task<int> GetNumberOfSearchPostAsync(string str, string target)
        {
            switch (target)
            {
                case "Title":
                    return await _context.PostEntities
                        .Where(p => p.Title.Contains(str))
                        .CountAsync();
                case "Content":
                    return await _context.PostEntities
                        .Where(p => p.Content.Contains(str))
                        .CountAsync();
                case "Author":
                    return await _context.PostEntities
                        .Where(p => p.Author == str)
                        .CountAsync();
                default:
                    break;
            }
            return 0;
        }
        public async Task<int> GetNumberOfPostAsync()
        {
            return await _context.PostEntities.CountAsync();
        }

        public async Task<PostCreateDto> GetPostByIdAsync(int id)
        {
            var temp = await _context.PostEntities.FindAsync(id);
            if(temp is null) { return new(); }
            return new PostCreateDto()
            {
                Title = temp.Title,
                Author = temp.Author,
                Content = temp.Content
            };
        }

        //U
        public async Task<bool> UpdatePostAsync(PostCreateDto dto, int i)
        {
            var pst = await _context.PostEntities.FindAsync(i);
            if (pst is null) return false;

            pst.Title = dto.Title;
            pst.Content = dto.Content;
            pst.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        //D
        public async Task DeletePost(int id)
        {
            var pst = await _context.PostEntities.FindAsync(id);
            if (pst is not null) { _context.PostEntities.Remove(pst); }
            await _context.SaveChangesAsync();
        }

    }
}
