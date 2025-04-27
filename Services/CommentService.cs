using Microsoft.EntityFrameworkCore;
using newIdManager.Data;
using newIdManager.Data.Posts;

namespace newIdManager.Services
{
    public class CommentService
    {
        private ApplicationDbContext _context;
        private readonly PostService _postService;

        public CommentService(ApplicationDbContext context, PostService postService)
        {
            _postService = postService;
            _context = context;
        }

        //c
        public async Task AddComment(CommentEntity commentEntity)
        {
            await _context.CommentEntities.AddAsync(commentEntity);
            var pst = await _context.PostEntities.FindAsync(commentEntity.PostId);
            if (pst != null)
            {
                pst.CommentCount++;
            }
            await _context.SaveChangesAsync();
        }
        //r
        public async Task<List<CommentEntity>> GetAllComments(int postId)
        {
            return await _context.CommentEntities.Where(p => p.PostId == postId).ToListAsync();
        }
        public async Task<CommentEntity?> GetCommentByIdAsync(int id)
        {
            return await _context.CommentEntities.FindAsync(id);
        }

        //u
        public async Task UpdateComment(CommentEntity commentEntity)
        {
            _context.CommentEntities.Update(commentEntity);
            await _context.SaveChangesAsync();
        }

        //d
        public async Task DeleteComment(int id)
        {
            var cmt = await GetCommentByIdAsync(id);
            if(cmt is not null)
            {
                _context.CommentEntities.Remove(cmt);
                await _context.SaveChangesAsync();
            }

        }


    }
}
