using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    
    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task addPostAsync(string postBody, string postTitle, int userID)
    {
        Post post = new Post(postTitle, postBody, userID);
        await postRepository.AddASync(post);
        
        Console.WriteLine($"Post '{postTitle}' has been created successfully.");
    }
    
}