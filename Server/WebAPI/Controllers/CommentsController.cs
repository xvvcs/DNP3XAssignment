using DTOs.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }
    
    //POST https://localhost:7198/comments
    [HttpPost]
    public async Task<IResult> AddCommentAsync([FromBody] AddCommentDTO request)
    {
        Post? existingPost = await _postRepository.FindPostById(request.PostId);
        User? existingUser = await _userRepository.GetSingleAsync(request.UserId);
        if (existingPost == null)
        {
            return Results.NotFound($"Post with ID '{request.PostId}' not found.");
        }
        if(existingUser == null)
        {
            return Results.NotFound($"User with ID '{request.UserId}' not found.");
        }
        
        Comment comment = new Comment
        {
            Body = request.Body,
            PostId = request.PostId,
            UserId = request.UserId,
            Like = new List<int>(),   
            Dislike = new List<int>() 
        };
        await _commentRepository.AddAsync(comment);
        return Results.Created($"comments/{comment.Id}", comment);
    }
    
    //GET https://localhost:7198/comments/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleCommentAsync([FromRoute] int id)
    {
        try
        {
            Comment result = await _commentRepository.GetSingleAsync(id);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }
    //DELETE https://localhost:7198/comments/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
        return Results.NoContent();
    }
    
    //PUT https://localhost:7198/comments/{id}
    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateCommentAsync([FromRoute] int id, [FromBody] ReplaceCommentDTO request)
    {
        Comment existingComment = await _commentRepository.FindCommentById(id);
        Comment comment = new Comment
        {
            Id = id,
            Body = request.Body,
            PostId = existingComment.PostId,
            UserId = existingComment.UserId,
            Like = existingComment.Like, 
            Dislike = existingComment.Dislike
        };
        await _commentRepository.UpdateAsync(comment);
        return Results.Ok(comment);
    }
    //GET https://localhost:7198/comments/post/
    [HttpGet]
    public async Task<IResult> GetCommentsAsync([FromQuery] string? title, [FromQuery] int? commentId)
    {
        List<Comment> comments = _commentRepository.GetMany().ToList();
        if(!string.IsNullOrWhiteSpace(title))
        {
            comments = comments.Where(c => c.Body.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        if(commentId.HasValue)
        {
            comments = comments.Where(c => c.Id == commentId).ToList();
        }
        return Results.Ok(comments);
    }
    //POST https://localhost:7198/comments/{id}/like
    [HttpPost("{id:int} and {userID:int}/like")]
    public async Task<IResult> LikeCommentAsync([FromRoute] int id, [FromRoute] int userID)
    {
        Comment comment = await _commentRepository.FindCommentById(id);
        await _commentRepository.LikeAsync(comment, userID);
        return Results.Ok(comment);
    }
    //POST https://localhost:7198/comments/{id}/dislike
    [HttpPost("{id:int} and {userID:int}/dislike")]
    public async Task<IResult> DislikeCommentAsync([FromRoute] int id, [FromRoute] int userID)
    {
        Comment comment = await _commentRepository.FindCommentById(id);
        await _commentRepository.DisLikeAsync(comment, userID);
        return Results.Ok(comment);
    }
    
}