namespace redditWebAPI.Model;

public class Comment
{
    public int Id { get; set; }
    public string? CommentAuthor { get; set; }
    public string? CommentText { get; set; }
    public DateTime CommentPublishDate { get; set; } = DateTime.Now;
    public int CommentUpvotes { get; set; } = 0;
    public int CommentDownvotes { get; set; } = 0;
    public int PostId { get; set; }
    public Post? Post { get; set; }
}