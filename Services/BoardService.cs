using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newIdManager.Data;
using SQLitePCL;

namespace newIdManager.Services
{
    public class BoardService
    {
        private readonly ApplicationDbContext _context;
        public BoardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedBoardAsync()
        {
            if (!_context.Boards.Any())
            {
                var boards = new List<Board>
                {
                    //new Board {Title = "Weather",Url="/weather" },
                    //new Board {Title = "Error",Url="/error" },
                    //new Board {Title = "Home",Url="/" },
                    //new Board {Title = "Hello",Url="/hello"}
                    new Board {Title = "사용자 리스트",Url="/list?page=1", Img="bi bi-person-fill-nav-menu"},
                    new Board {Title = "회원가입",Url="/register", Img="bi bi-lock-nav-menu"},
                    new Board {Title = "게시판",Url="/posts/lists?page=1", Img="bi bi-list-nested-nav-menu"},
                    //new Board {Title = "",Url=""},

                    //new Board {Title = "",Url=""},
                    //new Board {Title = "",Url=""},
                    //new Board {Title = "",Url=""},
                    //new Board {Title = "",Url=""},
                    //new Board {Title = "",Url=""},

                };
                _context.Boards.AddRange(boards);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Board>> GetAllBoardAsync()
        {
            return await _context.Boards.ToListAsync();
        }
        public async Task AddBoardAsync(Board board)
        {
            //"bi bi-lock-nav-menu" aria-hidden="true"></span>
            //board.Img = $"<span class=\"{board.Img}\"></span>";
            Console.WriteLine(board.Img);
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }

        public async Task AddOutToInBoardIdAsync(int outboard, int inboard)
        {
            //board.Boards.Add(inboard);
            var Upboard = await _context.Boards.FindAsync(outboard);
            var Inboard = await _context.Boards.FindAsync(inboard);
            if (Upboard == null || Inboard == null) return;

            Upboard.Boards.Add(Inboard);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAllBoard()
        {
            var allBoards = await _context.Boards.ToListAsync();
            _context.Boards.RemoveRange(allBoards);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveBoardAsync(string str)
        {
            var board = await _context.Boards
                .Where(u => u.Title == str)
                .ToListAsync();
            _context.Boards.RemoveRange(board);
            await _context.SaveChangesAsync();
        }
    }
}

