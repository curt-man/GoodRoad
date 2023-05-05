using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface ICommentRepository
    {
        
        ICollection<Comment> GetComments();
        Comment GetComment(int id);

    }
}
