using redditWebAPI;
using Microsoft.EntityFrameworkCore;
using redditWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskContext>(options => 
    options.UseSqlite("Data Source=tasks.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Tilføj testdata hvis databasen er tom

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskContext>();

    if (!db.Posts.Any())
    {
        var post = new Post
        {
            Author = "Hans Høns",
            Title = "Min første post",
            Content = "Jeg har høns",
            Comments = new List<Comment>
            {
                new Comment { CommentAuthor = "GretheHaderHøns", CommentText = "Første kommentar", CommentUpvotes = 101, CommentDownvotes = 5},
                new Comment { CommentAuthor = "Vicky", CommentText = "Anden kommentar", CommentUpvotes = 10, CommentDownvotes = 51 }
            }
        };
        
        var post2 = new Post
        {
            Author = "Hans Høns anden profil",
            Title = "Min andet post",
            Content = "Jeg har stadig høns",
            Comments = new List<Comment>
            {
                new Comment { CommentAuthor = "AlfredHaderOgsåHøns", CommentText = "Tredje kommentar", CommentUpvotes = 18, CommentDownvotes = 5},
                new Comment { CommentAuthor = "Bukser", CommentText = "Fjerde kommentar", CommentUpvotes = 83, CommentDownvotes = 15 }
            }
        };

        db.Posts.AddRange(post, post2);
        db.SaveChanges();
    }
}

// app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
