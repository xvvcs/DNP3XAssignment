using DTOs.Posts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    //POST https://localhost:7198/posts
    [HttpPost]
    public async Task<IResult> AddPostAsync([FromBody] AddPostsDTO request)
    {
        User? existingUser = await _userRepository.GetSingleAsync(request.UserId);
        if (existingUser == null)
        {
            return Results.NotFound($"User with ID '{request.UserId}' not found.");
        }
        Post post = new Post
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
            Like = new List<int>(),
            Dislike = new List<int>()
        };
        await _postRepository.AddASync(post);
        return Results.Created($"posts/{post.Id}", post);
    }
    
    //GET https://localhost:7198/posts/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSinglePostAsync([FromRoute] int id)
    {
        try
        {
            Post? result = await _postRepository.GetSingleAsync(id);
            if (result == null)
            {
                return Results.NotFound($"Post with ID {id} not found.");
            }
            List<Comment> comments = await _commentRepository.GetCommentsByPostIdAsync(id);
            var postWithComments = new
            {
                Post = result,
                Comments = comments
            };
            return Results.Ok(postWithComments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }
    
    //DELETE https://localhost:7198/posts/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeletePostAsync(int id)
    {
        await _postRepository.DeleteAsync(id);
        return Results.NoContent();
    }
    
    //PUT https://localhost:7198/posts/{id}
    [HttpPut("{id:int}")]
    public async Task<IResult> UpdatePostAsync([FromRoute] int id, [FromBody] ReplacePostsDTO request)
    {
        Post existingPost = await _postRepository.FindPostById(id);
        Post post = new Post
        {
            Id = id,
            Title = request.Title,
            Body = request.Body,
            UserId = existingPost.UserId,
            Like = existingPost.Like,
            Dislike = existingPost.Dislike
        };
        await _postRepository.UpdateAsync(post);
        return Results.Ok(post);
    }
    
    // GET https://localhost:7198/posts
    [HttpGet]
    public async Task<IResult> GetPostsAsync([FromQuery] string? titleContains, [FromQuery] int? userId)
    {
        List<Post> posts =  _postRepository.GetMany().ToList();

        if (!string.IsNullOrWhiteSpace(titleContains))
        {
            posts = posts.Where(p => p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (userId.HasValue)
        {
            posts = posts.Where(p => p.UserId == userId).ToList();
        }

        return Results.Ok(posts);
    }
    
    //POST https://localhost:7198/posts/{id}/like
    [HttpPost("{id:int} and {userID:int}/like")]
    public async Task<IResult> LikePostAsync([FromRoute] int id, [FromRoute] int userID)
    {
        Post post = await _postRepository.FindPostById(id);
        if (post == null)
        {
            return Results.NotFound($"Post with ID {id} not found.");
        }
        await _postRepository.LikeAsync(post, userID);
        return Results.Ok(post);
    }
    
    //POST https://localhost:7198/posts/{id}/dislike
    [HttpPost("{id:int} and {userID:int}/dislike")]
    public async Task<IResult> DislikePostAsync([FromRoute] int id, [FromRoute] int userID)
    {
        Post post = await _postRepository.FindPostById(id);
        if (post == null)
        {
            return Results.NotFound($"Post with ID {id} not found.");
        }
        await _postRepository.DisLikeAsync(post, userID);
        return Results.Ok(post);
    }
    
    // GET https://localhost:7198/posts/{postId}/comments
    [HttpGet("{postId:int}/comments")]
    public async Task<IResult> GetCommentsByPostIdAsync([FromRoute] int postId)
    {
        try
        {
            List<Comment> comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return Results.Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }
    
    
}