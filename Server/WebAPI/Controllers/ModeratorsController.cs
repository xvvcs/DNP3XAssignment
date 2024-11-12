using DTOs.Moderators;
using DTOs.SubForum;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ModeratorsController : ControllerBase
{
    private readonly IModeratorRepository _moderatorRepository;

    public ModeratorsController(IModeratorRepository moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    [HttpPost]
    public async Task<IResult> AddModeratorAsync([FromBody] AddModeratorDTO request)
    {
        Moderator moderator = new Moderator
        {
            UserId = request.UserId,
            SubForumIds = request.SubForumIds
        };
        await _moderatorRepository.AddAsync(moderator);
        return Results.Created($"Moderators/{moderator.Id}", moderator);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleModeratorAsync([FromRoute] int id)
    {
        try
        {
            Moderator? result = await _moderatorRepository.GetSingleAsync(id);
            if (result == null)
            {
                return Results.NotFound($"Moderator with ID {id} not found");
            }

            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteModeratorAsync(int id)
    {
        await _moderatorRepository.DeleteAsync(id);
        return Results.NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateModeratorAsync([FromRoute] int id, [FromBody] ReplaceModeratorDTO request)
    {
        Moderator existingModerator = await _moderatorRepository.GetSingleAsync(id);
        if (existingModerator == null)
        {
            return Results.NotFound($"Moderator with ID {id} not found.");
        }

        existingModerator.UserId = request.UserId;
        existingModerator.SubForumIds = request.SubForumIds;

        await _moderatorRepository.UpdateAsync(existingModerator);

        return Results.Ok(existingModerator);
    }

    [HttpGet]
    public async Task<IResult> GetModeratorsAsync([FromQuery] int? subForumId, [FromQuery] int? userId)
    {
        IQueryable<Moderator> moderatorsQuery = _moderatorRepository.GetManyAsync();

        if (subForumId.HasValue && subForumId != 0)
        {
            moderatorsQuery = moderatorsQuery.Where(m => m.SubForumIds.Contains(subForumId.Value));
        }

        if (userId.HasValue)
        {
            moderatorsQuery = moderatorsQuery.Where(m => m.UserId == userId);
        }

        List<Moderator> moderators = moderatorsQuery.ToList();

        return Results.Ok(moderators);
    }

    [HttpGet("{id:int}/creator")]
    public async Task<IResult> GetModeratorCreatorAsync([FromRoute] int id)
    {
        Moderator? moderator = await _moderatorRepository.GetSingleAsync(id);
        if (moderator == null)
        {
            return Results.NotFound($"No creator found for Moderator with ID {id}.");
        }

        return Results.Ok(new { CreatorId = moderator });
    }
    
    [HttpGet("{id:int}/subforums")]
    public async Task<IResult> GetModeratorsBySubForumIdAsync([FromRoute] int id)
    {
        List<Moderator> moderators = await _moderatorRepository.GetModeratorsBySubForumIdAsync(id);
        if(moderators.Count == 0)
        {
            return Results.NotFound($"No moderators found for SubForum with ID {id}.");
        }
        return Results.Ok(moderators);
    }
}