In this assignment series you will build out a small forum app. For each assignment you will add something new to the project, and at the end of the semester you will have a fully working forum app. Hopefully. It is very much inspired by Reddit.

In the end it will consist of a simple CRUD focused Web API, with Entity Framework Core and a SQLite database to store data. For the front-end you will have a Blazor web app.

In this first assignment you will create the entities for your domain model, and you will define repository interfaces (repository is explained later).

Because your app will evolve over time - different parts will be added and swapped out - we need to design the system with modularity in mind. We do this by creating multiple projects, each responsible for something specific. It will be a simplified layered application.

The Web API (i.e. server) will contain two layers: a network and a persistence. Often you will have a business logic layer in between, but we are skipping that, and simplifying the server to focus on the .NET tools rather than good SOLID architecture design. You should probably have this extra layer in your semester project.

There is some initial setup, which is best done on one computer, then shared to others through GitHub. I recommend reading through the entire document before actually starting on anything.

# 1. Features

This assignment is open-ended, meaning we provide you with a few minimum requirements, which must be completed. We also have suggestions on how to expand upon the system, should you wish to. Or you can come up with your own ideas.

### Feature description

We need a User, having at least user name, and a password. It needs an Id of type int.

We need a Post. It is written by a User. It contains a Title and a Body. It also needs an Id, of type int.

A User can also write a Comment on a Post. A Comment just contains a Body, and an Id of type int.

All entities must have an Id of type int.

The way we create relationships between the Entities is described in detail further below. In short, we use foreign keys, rather than associations.

Optional features

### Further feature suggestions, if you are brave:

· A user can like/dislike a post.

· A user can like/dislike a comment.

· A user can create a SubForum, meaning a post now belongs to a specific SubForum instead.

· A user can comment on a comment.

· A user becomes moderator of a sub forum, they created, meaning they can delete comments and posts.

· Feel free to add further ideas yourself.

Domain Model:
![Alt text](https://raw.githubusercontent.com/xvvcs/DNP3XAssignment1/master/DomainModelAssigment1.png)
