using Entities;

namespace RepositoryContracts;

public interface ISubForumRepository
{
    //Task<SubForum> AddAsync(SubForum subForum); // maybe change to createAsync?
    Task<SubForum> AddASync(SubForum subForum);
    Task UpdateAsync(SubForum subForum);
    Task DeleteAsync(int id);
    Task<SubForum> GetSingleAsync(int id);
    IQueryable<SubForum> GetMany();
    public Task<int> FindSubForumCreator(int subForumID);
}