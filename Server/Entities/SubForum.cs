namespace Entities;

public class SubForum
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public List<int> PostIds { get; set; } = new List<int>();
    
    public SubForum() { }
    public SubForum(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        PostIds = new List<int>();
    }
    public void AddPost(int postId)
    {
        if (!PostIds.Contains(postId))
        {
            PostIds.Add(postId);
        }
    }

    public void DeletePost(int postId)
    {
        if (PostIds.Contains(postId))
        {
            PostIds.Remove(postId);
        }
    }

}