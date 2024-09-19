// See https://aka.ms/new-console-template for more information
using CLI.UI;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();
IModeratorRepository moderatorRepository = new ModeratorInMemoryRepository();
ISubForumRepository subForumRepository = new SubForumInMemoryRepository();

var cliApp = new CliApp(userRepository, commentRepository, postRepository, moderatorRepository, subForumRepository);
await cliApp.StartAsync();
