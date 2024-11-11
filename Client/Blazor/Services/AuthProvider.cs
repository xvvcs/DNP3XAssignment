using System.Security.Claims;
using System.Text.Json;
using DTOs;
using DTOs.Posts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Blazor.Services;

public class AuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime JsRuntime;

    public AuthProvider(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        JsRuntime = jsRuntime;
    }

    public async Task Login(string username, string password)
    {
        HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(
            "auth/login",
            new LoginRequestDTO(username, password));
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        UserDTO user = JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException();

        string serializedData = JsonSerializer.Serialize(user);
        await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Id", user.Id.ToString()),
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string userAsJson = "";
        try
        {
            userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        if (string.IsNullOrEmpty(userAsJson))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(userAsJson);
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString()),
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    public async Task Logout()
    {
        await JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    public async Task Register(string username, string password)
    {
        HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(
            "auth/register",
            new AddUserDTO()
            {
                Username = username,
                Password = password
            });
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}