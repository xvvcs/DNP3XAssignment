// See https://aka.ms/new-console-template for more information
using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository();
IModeratorRepository moderatorRepository = new ModeratorInMemoryRepository();

var cliApp = new CliApp(userRepository, commentRepository, postRepository, moderatorRepository);
await cliApp.StartAsync();
