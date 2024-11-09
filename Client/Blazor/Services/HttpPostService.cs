using DTOs.Posts;

namespace Blazor.Services
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient _client;

        public HttpPostService(HttpClient client)
        {
            _client = client;
        }

        public async Task<PostDTO> AddPostAsync(PostDTO post)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("posts", post);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PostDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error adding post: " + ex.Message, ex);
            }
        }

        public async Task<PostDTO> GetPostAsync(int id)
        {
            try
            {
                return await _client.GetFromJsonAsync<PostDTO>($"posts/{id}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching post: " + ex.Message, ex);
            }
        }

        public async Task<PostDTO> UpdatePostAsync(PostDTO post)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync($"posts/{post.Id}", post);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PostDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error updating post: " + ex.Message, ex);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"posts/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error deleting post: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<PostDTO>> GetPostsAsync()
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<PostDTO>>("posts");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching posts: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<PostDTO>> GetPostsAsync(int userId)
        {
            try
            {
                return await _client.GetFromJsonAsync<IEnumerable<PostDTO>>($"users/{userId}/posts");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching user posts: " + ex.Message, ex);
            }
        }

        public async Task<List<CommentDTO>> GetCommentsAsync(int postId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"posts/{postId}/comments");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<CommentDTO>(); // Return an empty list if comments are not found
                }
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<CommentDTO>>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching comments: " + ex.Message, ex);
            }
        }

        public async Task<PostDTO> LikePostAsync(int postId, int userId)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"posts/{postId}/like?userId={userId}", null);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PostDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error liking post: " + ex.Message, ex);
            }
        }

        public async Task<PostDTO> DislikePostAsync(int postId, int userId)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsync($"posts/{postId}/dislike?userId={userId}", null);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PostDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error disliking post: " + ex.Message, ex);
            }
        }
        public async Task<CommentDTO> AddCommentAsync(CommentDTO comment)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("comments", comment);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CommentDTO>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error adding comment: " + ex.Message, ex);
            }
        }
    }
}