namespace redditWebAPI.Model;

public class Post
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime PublishDate { get; set; } = DateTime.Now;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public int Upvotes { get; set; } = 0; //Nullet gør at vi starter med nul upvotes
    public int Downvotes { get; set; } = 0;
}