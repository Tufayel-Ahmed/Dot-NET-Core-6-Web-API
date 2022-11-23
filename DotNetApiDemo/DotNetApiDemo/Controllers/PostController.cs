using CoreApiResponse;
using DotNetApiDemo.Context;
using DotNetApiDemo.Interfaces.Manager;
using DotNetApiDemo.Manager;
using DotNetApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DotNetApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : BaseController
    {
        /*ApplicationDbContext _dbContext;
        PostManager _postManager;
        public PostController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _postManager = new PostManager(dbContext);
        }*/

        IPostManager _postManager;
        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                //var posts = _dbContext.Posts.ToList();
                var posts = _postManager.GetAll().OrderBy(c => c.CreatedDate).ToList();
                return CustomResult("Data loaded successfully", posts, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetAllDSC()
        {
            try
            {
                //var posts = _dbContext.Posts.ToList();
                var posts = _postManager.GetAll().OrderByDescending(c => c.CreatedDate).ThenByDescending(c => c.Title).ToList();
                return CustomResult("Data loaded successfully", posts, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetAllData(string title)
        {
            try
            {
                var posts = _postManager.GetAllData(title);
                return CustomResult("Data loaded done", posts, HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult SearchPost(string text)
        {
            try
            {
                var posts = _postManager.SearchPost(text);
                return CustomResult("Searching Result: ", posts, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public IActionResult GetPosts(int page = 1)
        {
            try
            {
                var posts = _postManager.GetPosts(page, 2);
                return CustomResult($"Paging data for page number {page}: ", posts, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            try
            {
                var post = _postManager.GetById(id);
                if (post == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data found", post, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public IActionResult Add(Post post)
        {
            try
            {
                post.CreatedDate = DateTime.Now;
                bool isSaved = _postManager.Add(post);
                //_dbContext.Posts.Add(post);
                //bool isSaved = _dbContext.SaveChanges() > 0;
                if (isSaved)
                {
                    return CustomResult("Post has been created", post, HttpStatusCode.OK);
                }
                return CustomResult("Post save failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public IActionResult Edit(Post post)
        {
            try
            {
                if (post.Id == 0)
                {
                    return CustomResult("Id is missing", HttpStatusCode.BadRequest);
                }
                bool isUpdate = _postManager.Update(post);
                if (isUpdate)
                {
                    return CustomResult("Post is updated", post, HttpStatusCode.OK);
                }
                return CustomResult("Updated failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                var post = _postManager.GetById(id);
                if (post == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                bool isDelete = _postManager.Delete(post);
                if (isDelete)
                {
                    return CustomResult("Post has been deleted", HttpStatusCode.OK);
                }
                return CustomResult("Post delete action has been canceled", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
