using DTOs.SubForum;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubForumsController : ControllerBase
{
    private readonly ISubForumRepository _subForumRepository;

    public SubForumsController(ISubForumRepository subForumRepository)
    {
        _subForumRepository = subForumRepository;
    }

    // POST https://localhost:7198/subforums
    [HttpPost]
    public async Task<IResult> AddSubForumAsync([FromBody] AddSubForumDTO request)
    {
        SubForum subForum = new SubForum
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId,
            PostId = request.PostId
        };
        await _subForumRepository.AddASync(subForum);
        return Results.Created($"subforums/{subForum.Id}", subForum);
    }

    // GET https://localhost:7198/subforums/{id}
    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleSubForumAsync([FromRoute] int id)
    {
        try
        {
            SubForum? result = await _subForumRepository.GetSingleAsync(id);
            if (result == null)
            {
                return Results.NotFound($"SubForum with ID {id} not found.");
            }

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    // DELETE https://localhost:7198/subforums/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteSubForumAsync(int id)
    {
        await _subForumRepository.DeleteAsync(id);
        return Results.NoContent();
    }

    // PUT https://localhost:7198/subforums/{id}
    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateSubForumAsync([FromRoute] int id, [FromBody] ReplaceSubForumDTO request)
    {
        SubForum existingSubForum = await _subForumRepository.GetSingleAsync(id);
        if (existingSubForum == null)
        {
            return Results.NotFound($"SubForum with ID {id} not found.");
        }

        existingSubForum.Title = request.Title;
        existingSubForum.Description = request.Description;

        await _subForumRepository.UpdateAsync(existingSubForum);
        return Results.Ok(existingSubForum);
    }

    // GET https://localhost:7198/subforums
    [HttpGet]
    public async Task<IResult> GetSubForumsAsync([FromQuery] string? titleContains, [FromQuery] int? userId)
    {
        IQueryable<SubForum> subForumsQuery = _subForumRepository.GetMany();

        if (!string.IsNullOrWhiteSpace(titleContains))
        {
            subForumsQuery = subForumsQuery.Where(sf => sf.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase));
        }

        if (userId.HasValue)
        {
            subForumsQuery = subForumsQuery.Where(sf => sf.UserId == userId);
        }

        List<SubForum> subForums = subForumsQuery.ToList();
        // List<SubForum> subForums = await subForumsQuery.ToListAsync();
        return Results.Ok(subForums);
    }

    // GET https://localhost:7198/subforums/{id}/creator
    [HttpGet("{id:int}/creator")]
    public async Task<IResult> GetSubForumCreatorAsync([FromRoute] int id)
    {
        int creatorId = await _subForumRepository.FindSubForumCreator(id);
        if (creatorId == 0)
        {
            return Results.NotFound($"No creator found for SubForum with ID {id}.");
        }

        return Results.Ok(new { CreatorId = creatorId });
    }
}
