using DotNetApiDemo.Context;
using DotNetApiDemo.Interfaces.Manager;
using DotNetApiDemo.Models;
using DotNetApiDemo.Repository;
using EF.Core.Repository.Interface.Repository;
using EF.Core.Repository.Manager;

namespace DotNetApiDemo.Manager
{
    public class PostManager : CommonManager<Post>, IPostManager
    {
        public PostManager(ApplicationDbContext applicationDbContext) : base(new PostRepository(applicationDbContext))
        {

        }

        public ICollection<Post> GetAllData(string title)
        {
            return Get(c => c.Title.ToLower() == title.ToLower());
        }

        public Post GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public ICollection<Post> GetPosts(int page, int pagesize)
        {
            if(page <= 1)
            {
                page = 0;
            }
            int totalNumber = page * pagesize;
            return GetAll().Skip(totalNumber).Take(page).ToList();
        }

        public ICollection<Post> SearchPost(string text)
        {
            text = text.ToLower();
            return Get(c => c.Title.ToLower().Contains(text) || c.Description.ToLower().Contains(text));
        }
    }
}
