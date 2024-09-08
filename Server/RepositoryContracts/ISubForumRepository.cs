﻿using Entities;

namespace RepositoryContracts;

public interface ISubForumRepository
{
    Task<SubForum> AddAsync(SubForum subForum); // maybe change to createAsync?
    Task UpdateAsync(SubForum subForum);
    Task DeleteAsync(int id);
    Task<SubForum> GetSingleAsync(int id);
    IQueryable<SubForum> GetManyAsync();
}