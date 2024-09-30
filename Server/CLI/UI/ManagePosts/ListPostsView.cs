using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    
    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public Task DisplayPosts()
    {
        Console.WriteLine("Listing all posts:");
        foreach (Post post in postRepository.GetMany())
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Content: {post.Body}, Like Count: {post.Like}, Dislike Count: {post.Dislike}");
        }
        return Task.CompletedTask;
    }
    
    

}