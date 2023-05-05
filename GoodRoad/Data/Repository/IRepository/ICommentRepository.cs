using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface ICommentRepository
    {
        
        ICollection<Comment> GetComments();
        ICollection<Comment> GetCommentsByHole(int id);
        Comment GetComment(int id);

        bool CreateComment(Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(Comment comment);

        bool Save();
    }
}
