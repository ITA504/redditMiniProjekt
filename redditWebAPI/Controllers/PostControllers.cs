using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using redditWebAPI;
using redditWebAPI.Model;

namespace redditWebAPIApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly TaskContext _context;

    public PostsController(TaskContext context)
    {
        _context = context;
    }
    
    // ***** GET forespørgelser *****
    
    // GET: api/posts
    // Henter alle posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        return await _context.Posts
            .Include(p => p.Comments)
            .ToListAsync();
    }
    
    // GET: api/posts/id
    // Eksempelvis /api/posts/1
    // Dette henter et bestemt post med dets kommentarer
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _context.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
        return post;
    }
    
    // ***** PUT forespørgelser *****
    
    // PUT: api/posts/id/upvote
    // Bruges til at upvote
    [HttpPut("{id}/upvote")]
    public async Task<ActionResult<Post>> UpvotePost(int id)
    {
        var post  = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        post.Upvotes++;
        await _context.SaveChangesAsync();

        return Ok(post.Upvotes);
    }
    
    // PUT: api/posts/id/downvote
    // Bruges til at downvote
    [HttpPut("{id}/downvote")]
    public async Task<ActionResult<Post>> DownvotePost(int id)
    {
        var post  = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        post.Downvotes++;
        await _context.SaveChangesAsync();

        return Ok(post.Downvotes);
    }
    
    // PUT: api/posts/postid/comments/commentid/upvote
    // Bruges til at upvote en comment
    [HttpPut("{postid}/comments/{commentid}/upvote")]
    public async Task<ActionResult<Comment>> UpvoteComment(int commentid)
    {
        var comment = await _context.Comments.FindAsync(commentid);

        if (comment == null)
        {
            return NotFound();
        }

        comment.CommentUpvotes++;
        await _context.SaveChangesAsync();

        return Ok(comment.CommentUpvotes);
    }
    
    // PUT: api/posts/postid/comments/commentid/downvote
    // Bruges til at downvote en comment
    [HttpPut("{postid}/comments/{commentid}/downvote")]
    public async Task<ActionResult<Comment>> DownvoteComment(int commentid)
    {
        var comment = await _context.Comments.FindAsync(commentid);

        if (comment == null)
        {
            return NotFound();
        }

        comment.CommentDownvotes++;
        await _context.SaveChangesAsync();

        return Ok(comment.CommentDownvotes);
    }
    
    // ***** POST forespørgsler *****
    
    // POST: api/posts
    // Bruges til at poste et post
    [HttpPost]
    public async Task<ActionResult<Post>> Post(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }
    
    // POST: api/posts/id/comments
    // Bruges til at lave en comment
    [HttpPost("{postid}/comments")]
    public async Task<ActionResult<Comment>> PostComment(int postid, Comment comment)
    {
        comment.PostId = postid;
        
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetPost), new { id = postid }, comment);
    }
    
}