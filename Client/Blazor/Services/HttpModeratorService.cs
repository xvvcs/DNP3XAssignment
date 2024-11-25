using System.Text.Json;
using System.Net.Http.Json;
using DTOs.Moderators;
using DTOs.Posts;
using DTOs.SubForum;

namespace Blazor.Services
{
    public class HttpModeratorService : IModeratorService
    {
        private readonly HttpClient client;

        public HttpModeratorService(HttpClient client)
        {
            this.client = client;
        }
        
        public async Task<AddModeratorDTO> AddModeratorAsync(AddModeratorDTO moderator)
        {
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync("moderators", moderator);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<AddModeratorDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }
        

        public async Task<ModeratorDTO> GetModeratorAsync(int id)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"moderators/{id}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<ModeratorDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }

        public async Task<ModeratorDTO> GetModeratorAsync(string username)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"moderators/username/{username}");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<ModeratorDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }
        
        public async Task<ModeratorDTO> UpdateModeratorAsync(ModeratorDTO moderator)
        {
            HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"moderators/{moderator.Id}", moderator);
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<ModeratorDTO>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }
        
        public async Task DeleteModeratorAsync(int id)
        {
            HttpResponseMessage httpResponse = await client.DeleteAsync($"moderators/{id}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }
        }
        
        public async Task<IEnumerable<ModeratorDTO>> GetModeratorAsync()
        {
            HttpResponseMessage httpResponse = await client.GetAsync("moderators");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<IEnumerable<ModeratorDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }

        public async Task<IEnumerable<PostDTO>> GetPostsAsync(int userId)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"moderators/{userId}/posts");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<IEnumerable<PostDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }
        
        public async Task<IEnumerable<SubforumDTO>> GetSubforumsAsync(int moderatorId)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"moderators/{moderatorId}/subforums");
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

        public async Task<List<ModeratorDTO>> GetModeratorsBySubForumIdAsync(int subForumId)
        {
            HttpResponseMessage httpResponse = await client.GetAsync($"moderators/{subForumId}/subforums");
            string response = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(response);
            }
            return JsonSerializer.Deserialize<List<ModeratorDTO>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to deserialize response.");
        }

        public async Task AssignModeratorToSubforumsAsync(int moderatorId, List<int> subforumIds)
        {
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"moderators/{moderatorId}/subforums/assign", subforumIds);
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }
        }

        public async Task RemoveModeratorFromSubforumsAsync(int moderatorId, List<int> subforumIds)
        {
            HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"moderators/{moderatorId}/subforums/remove", subforumIds);
            if (!httpResponse.IsSuccessStatusCode)
            {
                string response = await httpResponse.Content.ReadAsStringAsync();
                throw new Exception(response);
            }
        }
    }
}
