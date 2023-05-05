using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;

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
    }
}
