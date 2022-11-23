using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseInMemoryDatabase("TestDB");
    options.UseSqlServer(builder.Configuration.GetConnectionString("MinimalDbConnection"));
});

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello World";
});

app.MapPost("/api/post", (Post post) =>
{
    return Results.Ok(post);
});

app.MapPut("/", () =>
{
    return Results.Ok("Call put action method");
});

app.MapDelete("/", () =>
{
    return Results.Ok("Call delete action method");
});

app.MapGet("/posts", (ApplicationDbContext db) =>
{
    var posts = db.Posts.ToList();
    return Results.Ok(posts);
});

app.MapPost("/posts", (Post post, ApplicationDbContext db) =>
{
    db.Posts.Add(post);
    bool isSaved = db.SaveChanges() > 0;
    if (isSaved)
    {
        return Results.Ok("Post has been saved");
    }
    return Results.BadRequest("Post saved failed");
});

app.MapPut("/posts", (int id, Post post, ApplicationDbContext db) =>
{
    var data = db.Posts.FirstOrDefault(c => c.Id == id);
    if(data == null)
    {
        return Results.NotFound();
    }
    if(data.Id != post.Id)
    {
        return Results.BadRequest("Id not valid");
    }
    data.Title = post.Title;
    data.Description = post.Description;
    bool isUpdated = db.SaveChanges() > 0;
    if (isUpdated)
    {
        return Results.Ok("Post has been modified");
    }
    return Results.BadRequest("Post modified failed");
});

app.MapDelete("/posts", (int id, ApplicationDbContext db) =>
{
    var post = db.Posts.FirstOrDefault(c => c.Id == id);
    if (post == null)
    {
        return Results.NotFound();
    }
    if (post.Id != post.Id)
    {
        return Results.BadRequest("Id not valid");
    }
    db.Posts.Remove(post);
    bool isDeleted = db.SaveChanges() > 0;
    if (isDeleted)
    {
        return Results.Ok("Post has been deleted");
    }
    return Results.BadRequest("Post deleted failed");
});

app.Run();