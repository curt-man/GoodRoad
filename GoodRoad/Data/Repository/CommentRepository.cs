using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodRoad.Data.Repository
{
    
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Comment GetComment(int id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Comment> GetComments()
        {
            return _dbContext.Comments.OrderBy(p=>p.Id).ToList();
        }

        public ICollection<Comment> GetCommentsByHole(int id)
        {
            return _dbContext.Comments.Where(x=>x.HoleId == id).OrderBy(p => p.Id).ToList();
        }

        public bool CreateComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            return Save();
        }


        public bool UpdateComment(Comment comment)
        {
            _dbContext.Comments.Update(comment);
            return Save();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() != 0;
        }
    }
}
