using DTOs.Posts;

namespace Blazor.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(PostDTO post);
    public Task<PostDTO> GetPostAsync(int id);
    public Task<PostDTO> UpdatePostAsync(PostDTO post);
    public Task DeletePostAsync(int id);
    public Task<IEnumerable<PostDTO>> GetPostsAsync();
    public Task<IEnumerable<PostDTO>> GetPostsAsync(int userId);
    public Task<List<CommentDTO>> GetCommentsAsync(int postId);
    public Task<PostDTO> LikePostAsync(int postId, int userId);
    public Task<PostDTO> DislikePostAsync(int postId, int userId);
    public Task<CommentDTO> AddCommentAsync(CommentDTO comment);
}