using DTOs.Moderators;
using DTOs.SubForum;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;


namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ModeratorsController: ControllerBase
{
    private readonly IModeratorRepository _moderatorRepository;

    public ModeratorsController(IModeratorRepository moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }
    
    // POST https://localhost:7198/moderators
    [HttpPost]
    public async Task<IResult> AddModeratorAsync([FromBody] AddModeratorDTO request)
    {
        Moderator moderator = new Moderator
        {
            UserId = request.UserId,
            SubForumId = request.SubForumId
        };
        await _moderatorRepository.AddAsync(moderator);
        return Results.Created($"Moderators/{moderator.Id}", moderator);
    }
    
    // GET https://localhost:7198/subforums/{id}
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
    
    // DELETE https://localhost:7198/moderators/{id}
    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteModeratorAsync(int id)
    {
        await _moderatorRepository.DeleteAsync(id);
        return Results.NoContent();
    }
    
    // PUT https://localhost:7198/moderators/{id}
    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateModeratorAsync([FromRoute] int id, [FromBody] ReplaceModeratorDTO request)
    {
        Moderator existingModerator = await _moderatorRepository.GetSingleAsync(id);
        if (existingModerator == null)
        {
            return Results.NotFound($"Moderator with ID {id} not found. ");
        }

        // Update moderator's properties using the DTO
        existingModerator.UserId = request.UserId;
        existingModerator.SubForumId = request.SubForumId;
        
        await _moderatorRepository.UpdateAsync(existingModerator);
        
        return Results.Ok(existingModerator);
    }
    
    // GET https://localhost:7198/subforums
    [HttpGet]
    public async Task<IResult> GetModeratorsAsync([FromQuery] int? subForumId, [FromQuery] int? userId)
    {
        IQueryable<Moderator> moderatorsQuery = _moderatorRepository.GetManyAsync();

        // Filter by subForumId if it's provided (not null or 0)
        if (subForumId.HasValue && subForumId != 0)
        {
            moderatorsQuery = moderatorsQuery.Where(m => m.SubForumId == subForumId);
        }

        // Filter by userId if it's provided
        if (userId.HasValue)
        {
            moderatorsQuery = moderatorsQuery.Where(m => m.UserId == userId);
        }

        // Fetch the filtered results asynchronously
        List<Moderator> moderators = moderatorsQuery.ToList();

        return Results.Ok(moderators);
    }
    
    // GET https://localhost:7198/moderators/{id}/creator
    [HttpGet("{id:int}/creator")]
    public async Task<IResult> GetModeratorCreatorAsync([FromRoute] int id)
    {
        Moderator? moderator = await _moderatorRepository.GetSingleAsync(id);
        // If no creator is found (assuming 0 means no valid ID)
        if (moderator == null)
        {
            return Results.NotFound($"No creator found for Moderator with ID {id}.");
        }

        // Return the found creator's ID
        return Results.Ok(new { CreatorId = moderator });
    }
}