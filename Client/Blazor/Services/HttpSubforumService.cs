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
    public async Task<SubforumDTO> AddSubforumAsync(SubforumDTO subforum)
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

    public async Task<SubforumDTO> GetPostAsync(int id)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"posts/{id}/subforum");
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
        ///TODO check if better to used PostID or have subforumId as currently "id"
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

    public async Task<IEnumerable<SubforumDTO>> GetsubforumAsync()
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

    public async Task<IEnumerable<PostDTO>> GetPostsAsync(int moderatorId)
    {
        HttpResponseMessage httpResponse = await _client.GetAsync($"moderators/{moderatorId}/posts");
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<PostDTO>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();
    }
}