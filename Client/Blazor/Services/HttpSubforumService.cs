using System.Text.Json;
using DTOs.Posts;
using DTOs.SubForum;

namespace Blazor.Services;

public class HttpSubforumService : ISubforumService
{
    private readonly HttpClient _client;
    
    public HttpSubforumService(HttpClient client)
    {
        _client = client;
    }
    public async Task<SubforumDTO> AddSubforumAsync(AddSubForumDTO subforum)
    {
        HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("subforums", subforum);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<SubforumDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
    }
    public async Task<SubforumDTO> GetSubforumAsync(int id)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"subforums/{id}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<SubforumDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
    }
    public async Task<SubforumDTO> UpdateSubforumAsync(SubforumDTO subforum)
    {
        HttpResponseMessage httpResponse = await _client.PutAsJsonAsync($"subforums/{subforum.Id}", subforum);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        if (string.IsNullOrWhiteSpace(response))
        {
            throw new Exception("The response content is empty.");
        }
        return JsonSerializer.Deserialize<SubforumDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Deserialization returned null.");
    }
    public async Task DeleteSubforumAsync(int id)
    {
        HttpResponseMessage httpResponse = await _client.DeleteAsync($"subforums/{id}");
        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }
    public async Task<IEnumerable<SubforumDTO>> GetSubforumsAsync()
    {
        HttpResponseMessage httpResponse = await _client.GetAsync("subforums");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<SubforumDTO>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
    }
    public async Task<IEnumerable<PostDTO>> GetPostsBySubforumAsync(int subforumId)
    {
        try
        {
            var response = await _client.GetAsync($"subforums/{subforumId}/posts");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<PostDTO>(); // Return an empty list if no posts are found
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<PostDTO>>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error fetching posts for subforum with ID {subforumId}: {ex.Message}", ex);
        }
    }
   
    public async Task AddPostsToSubforumAsync(int subforumId, int postId)
    {
        try
        {
            var response = await _client.PostAsync($"subforums/{subforumId}/posts/{postId}", null);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error adding post to subforum with ID {subforumId}: {ex.Message}", ex);
        }
    }

    public async Task DeletePostsFromSubforumAsync(int subforumId, int postId)
    {
        try
        {
            var response = await _client.DeleteAsync($"subforums/{subforumId}/posts/{postId}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error deleting post from subforum with ID {subforumId}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<SubforumDTO>> GetSubforumsByUserIdAsync(int userId)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"subforums/user/{userId}");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<SubforumDTO>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize response.");
    }
}