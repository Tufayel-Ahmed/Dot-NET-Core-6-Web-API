using DotNetApiDemo.Models;
using EF.Core.Repository.Interface.Manager;

namespace DotNetApiDemo.Interfaces.Manager
{
    public interface IPostManager: ICommonManager<Post>
    {
        Post GetById(int id);

        ICollection<Post> GetAllData(string title);

        ICollection<Post> SearchPost(string text);

        ICollection<Post> GetPosts(int page, int pagesize);

    }
}
