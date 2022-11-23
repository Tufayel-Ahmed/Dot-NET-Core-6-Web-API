using DotNetApiDemo.Models;
using EF.Core.Repository.Interface.Repository;

namespace DotNetApiDemo.Interfaces.Repository
{
    public interface IPostRepository: ICommonRepository<Post>
    {
    }
}
