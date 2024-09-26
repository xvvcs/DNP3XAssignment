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

    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    //POST https://localhost:7198/posts
    [HttpPost]
    public async Task<IResult> AddPostAsync([FromBody] AddPostsDTO request)
    {
        Post post = new Post
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.UserId,
            LikeCount = 0,
            DislikeCount = 0
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
            Post result = await _postRepository.GetSingleAsync(id);
            return Results.Ok(result);
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
            LikeCount = existingPost.LikeCount,
            DislikeCount = existingPost.DislikeCount
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
    [HttpPost("{id:int}/like")]
    public async Task<IResult> LikePostAsync(int id)
    {
        Post post = await _postRepository.FindPostById(id);
        post.LikeCount++;
        await _postRepository.LikeAsync(post);
        return Results.Ok(post);
    }
    
    //POST https://localhost:7198/posts/{id}/dislike
    [HttpPost("{id:int}/dislike")]
    public async Task<IResult> DislikePostAsync(int id)
    {
        Post post = await _postRepository.FindPostById(id);
        post.DislikeCount++;
        await _postRepository.DisLikeAsync(post);
        return Results.Ok(post);
    }
    
    
}