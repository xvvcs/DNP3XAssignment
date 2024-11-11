namespace DTOs.SubForum;

public class SubforumDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    
    public List<int> PostIds { get; set; } = new List<int>();
}