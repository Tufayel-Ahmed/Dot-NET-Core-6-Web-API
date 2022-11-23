using DotNetApiDemo.Context;
using DotNetApiDemo.Interfaces.Repository;
using DotNetApiDemo.Models;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNetApiDemo.Repository
{
    public class PostRepository : CommonRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
