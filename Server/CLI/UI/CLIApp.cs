using CLI.UI.ManageComments;
using CLI.UI.ManageModerators;
using CLI.UI.ManagePosts;
using CLI.UI.ManageSubForums;
using CLI.UI.ManageUsers;
using RepositoryContracts;

//TODO: Create dummy data for subFormus and moderators
//TODO: Create Log In and Log Out functionality
//TODO: Fix likes and dislikes, so you cannot like something infinitely
//TODO: Fix update comment and post, so that likes and dislikes are kept the same from origina comment/post


namespace CLI.UI
{
    public class CliApp
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly SinglePostView _singlePostView;
        private readonly ManagePostsView _managePostsView;
        
        private readonly CreateCommentsView _createCommentsView;
        private readonly ListCommentsView _listCommentsView;
        private readonly SingleCommentView _singleCommentView;
        private readonly ManageCommentsView _manageCommentsView;

        private readonly CreateUserView _createUserView;
        private readonly ListUsersView _listUsersView;
        private readonly SingleUserView _singleUserView;
        private readonly ManageUsersView _manageUsersView;
        
        private readonly CreateModeratorView _createModeratorView;
        private readonly ListModeratorsView _listModeratorsView;
        private readonly SingleModeratorView _singleModeratorView;
        private readonly ManageModeratorsView _manageModeratorsView;
        
        private readonly CreateSubForumView _createSubForumView;
        private readonly ListSubForumsView _listSubForumsView;
        private readonly SingleSubForumView _singleSubForumView;
        private readonly ManageSubForumsView _manageSubForumsView;

        public CliApp(
            IUserRepository userRepository, 
            ICommentRepository commentRepository, 
            IPostRepository postRepository,
            IModeratorRepository moderatorRepository,
            ISubForumRepository subForumRepository)
        {
            
            _createPostView = new CreatePostView(postRepository);
            _listPostsView = new ListPostsView(postRepository);
            _singlePostView = new SinglePostView(postRepository, commentRepository);
            _managePostsView = new ManagePostsView(postRepository);

            
            _createCommentsView = new CreateCommentsView(commentRepository);
            _listCommentsView = new ListCommentsView(commentRepository);
            _singleCommentView = new SingleCommentView(commentRepository);
            _manageCommentsView = new ManageCommentsView(commentRepository);
            
            _createUserView = new CreateUserView(userRepository);
            _listUsersView = new ListUsersView(userRepository);
            _singleUserView = new SingleUserView(userRepository);
            _manageUsersView = new ManageUsersView(userRepository);
            
            _createModeratorView = new CreateModeratorView(moderatorRepository);
            _listModeratorsView = new ListModeratorsView(moderatorRepository, userRepository, subForumRepository);
            _singleModeratorView = new SingleModeratorView(moderatorRepository, userRepository);
            _manageModeratorsView = new ManageModeratorsView(moderatorRepository);
            
            _createSubForumView = new CreateSubForumView(subForumRepository);
            _listSubForumsView = new ListSubForumsView(subForumRepository, moderatorRepository, userRepository);
            _singleSubForumView = new SingleSubForumView(subForumRepository, moderatorRepository);
            _manageSubForumsView = new ManageSubForumsView(subForumRepository);
        }
        
        public async Task StartAsync()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- CLI App Menu ---");
                Console.WriteLine("1. Manage Posts");
                Console.WriteLine("2. Manage Comments");
                Console.WriteLine("3. Manage Users");
                Console.WriteLine("4. Manage Moderators");
                Console.WriteLine("5. Manage Sub-Forums");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await ManagePostsMenu();
                        break;
                    case "2":
                        await ManageCommentsMenu();
                        break;
                    case "3":
                        await ManageUsersMenu();
                        break;
                    case "4":
                        await ManageModeratorsMenu();
                        break;
                    case "5":
                        await ManageSubForumsMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting CLI app...");
        }

        private async Task ManagePostsMenu()
        {
            Console.WriteLine("\n--- Manage Posts ---");
            Console.WriteLine("1. View All Posts");
            Console.WriteLine("2. View Single Post");
            Console.WriteLine("3. Create Post");
            Console.WriteLine("4. Update Post");
            Console.WriteLine("5. Delete Post");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await _listPostsView.DisplayPosts();
                    break;
                case "2":
                    Console.Write("Enter Post ID: ");
                    if (int.TryParse(Console.ReadLine(), out int postId))
                    {
                        await _singlePostView.DisplayPostAsync(postId);
                        
                        Console.WriteLine("\n--- Post Interaction ---");
                        Console.WriteLine("1. Like Post");
                        Console.WriteLine("2. Dislike Post");
                        Console.WriteLine("0. Back to Main Menu");
                        Console.Write("Choose an option: ");
                        var interactionInput = Console.ReadLine();           //TODO user can like and dislike the post infinitely. We need to track what user likes to avoid multiple liking and disliking of one post
                        switch (interactionInput)
                        {
                            case "1":
                                await _managePostsView.LikePostAsync(postId);
                                Console.WriteLine("You liked the post.");
                                break;
                            case "2":
                                await _managePostsView.DislikePostAsync(postId);
                                Console.WriteLine("You disliked the post.");
                                break;
                            case "0":
                                await StartAsync();
                                break;
                            default:
                                Console.WriteLine("Invalid option. Try again.");
                                break;
                        }
                    }
                    break;
                case "3":
                    Console.Write("Enter Post Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Enter Post Body: ");
                    var body = Console.ReadLine();
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        await _createPostView.addPostAsync(body, title, userId);
                    }
                    break;
                case "4": 
                    Console.Write("Enter Post ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int postToUpdateId))
                    {
                        Console.Write("Enter New Post Title: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("Enter New Post Body: ");
                        var newBody = Console.ReadLine();
                        Console.Write("Enter User ID: ");
                        if (int.TryParse(Console.ReadLine(), out int updateUserId))
                        {
                            await _managePostsView.UpdatePostAsync(postToUpdateId, newBody, newTitle, updateUserId);
                        }
                    }
                    break;
                case "5":
                    Console.Write("Enter Post ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int postToDeleteId))
                    {
                        await _managePostsView.DeletePostAsync(postToDeleteId);
                    }
                    break;
                case "0":
                    await StartAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        private async Task ManageCommentsMenu()
        {
            Console.WriteLine("\n--- Manage Comments ---");
            Console.WriteLine("1. View All Comments");
            Console.WriteLine("2. View Comments by Post ID");
            Console.WriteLine("3. View Single Comment");
            Console.WriteLine("4. Create Comment");
            Console.WriteLine("5. Update Comment");
            Console.WriteLine("6. Delete Comment");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await _listCommentsView.DisplayCommentsAsync();
                    break;
                case "2":
                    Console.Write("Enter Post ID to View Comments: ");
                    if (int.TryParse(Console.ReadLine(), out int postID))
                    {
                        await _listCommentsView.DisplayCommentsByIdAsync(postID);
                    }
                    break;
                case "3":
                    Console.Write("Enter Comment ID: ");
                    if (int.TryParse(Console.ReadLine(), out int SinglePostID))
                    {
                        await _singleCommentView.DisplayComment(SinglePostID);
                    }

                    break;
                case "4":
                    Console.Write("Enter Comment Body: ");
                    var body = Console.ReadLine();
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userID))
                    {
                        Console.Write("Enter Post ID: ");
                        if (int.TryParse(Console.ReadLine(), out int postId))
                        {
                            await _createCommentsView.CreateCommentAsync(body, userID, postId);
                        }
                    }
                    break;
                case "5": //TODO: CORRECT IT SO LIKES ARE KEPT THE SAME
                    Console.Write("Enter Comment ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int commentID))
                    {
                        Console.Write("Enter New Comment Body: ");
                        var newContent = Console.ReadLine();
                        Console.Write("Enter User ID: ");
                        if (int.TryParse(Console.ReadLine(), out int userId))
                        {
                            await _manageCommentsView.UpdateComment(commentID, newContent, userId);
                        }
                    }
                    break;
                case "6":
                    Console.Write("Enter Comment ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int commentId))
                    {
                        await _manageCommentsView.DeleteComment(commentId);
                    }
                    break;
                case "0":
                    await StartAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        private async Task ManageUsersMenu()
        {
            Console.WriteLine("\n--- Manage Users ---");
            Console.WriteLine("1. View All Users");
            Console.WriteLine("2. View Single User");
            Console.WriteLine("3. Create User");
            Console.WriteLine("4. Update User");
            Console.WriteLine("5. Delete User");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await _listUsersView.DisplayUsers();
                    break;
                case "2":
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        await _singleUserView.DisplayUserAsync(userId);
                    }
                    break;
                case "3":
                    Console.Write("Enter Username: ");
                    var username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    var password = Console.ReadLine();
                    await _createUserView.addUserAsync(username, password);
                    break;
                case "4":
                    Console.Write("Enter User ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int userIdToUpdate))
                    {
                        Console.Write("Enter New Username: ");
                        var newUsername = Console.ReadLine();
                        Console.Write("Enter New Password: ");
                        var newPassword = Console.ReadLine();
                        await _manageUsersView.UpdateUserAsync(newUsername, newPassword, userIdToUpdate);
                    }
                    break;
                case "5":
                    Console.Write("Enter User ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
                    {
                        await _manageUsersView.DelateUserAsync(userIdToDelete);
                    }
                    break;
                case "0":
                    await StartAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        private async Task ManageModeratorsMenu()
        {
            Console.WriteLine("\n--- Manage Moderators ---");
            Console.WriteLine("1. View All Moderators");
            Console.WriteLine("2. View All Moderators by Sub-Forum ID");
            Console.WriteLine("3. View Single Moderator");
            Console.WriteLine("4. Create Moderator");
            Console.WriteLine("5. Update Moderator");
            Console.WriteLine("6. Delete Moderator");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await _listModeratorsView.DisplayModeratorsAsync();
                    break;
                case "2":
                    Console.Write("Enter Sub-Forum ID to View Moderators: ");
                    if (int.TryParse(Console.ReadLine(), out int subForumId1))
                    {
                        await _listModeratorsView.DisplayModeratorsBySubForumIdAsync(subForumId1);
                    }

                    break;
                case "3":
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userId1))
                    {
                        await _singleModeratorView.DisplayModerator(userId1);
                    }

                    break;
                case "4":
                    Console.Write("Enter User ID: ");
                    if (int.TryParse(Console.ReadLine(), out int userId))
                    {
                        Console.Write("Enter Sub-Forum ID: ");
                        if (int.TryParse(Console.ReadLine(), out int subForumId))
                        {
                            await _createModeratorView.CreateModeratorAsync(userId, subForumId);
                        }
                    }

                    break;
                case "5":
                    Console.Write("Enter Moderator ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int moderatorID))
                    {
                        Console.Write("Enter New User ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newUserID))
                        {
                            Console.Write("Enter New Sub-Forum ID: ");
                            if (int.TryParse(Console.ReadLine(), out int newSubForumID))
                            {
                                await _manageModeratorsView.UpdateModerator(newUserID, moderatorID, newSubForumID);
                            }
                        }
                    }

                    break;
                case "6":
                    Console.Write("Enter Moderator ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int moderatorId))
                    {
                        await _manageModeratorsView.DeleteModerator(moderatorId);
                    }

                    break;
                case "0":
                    await StartAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }

        private async Task ManageSubForumsMenu()
        {
            Console.WriteLine("\n--- Manage Sub-Forums ---");
            Console.WriteLine("1. View All Sub-Forums");
            Console.WriteLine("2. View Single Sub-Forum");
            Console.WriteLine("3. Create Sub-Forum");
            Console.WriteLine("4. Update Sub-Forum");
            Console.WriteLine("5. Delete Sub-Forum");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Displaying all SubForums:");
                    await _listSubForumsView.DisplayAllSubForums();
                    break;
                case "2":
                    Console.Write("Enter Sub-Forum ID: ");
                    if (int.TryParse(Console.ReadLine(), out int subForumId))
                    {
                        await _singleSubForumView.DisplaySingleSubForum(subForumId);
                    }

                    break;
                case "3":
                    Console.Write("Enter Sub-Forum Title: ");
                    var title = Console.ReadLine();
                    Console.Write("Enter Sub-Forum Description: ");
                    var description = Console.ReadLine();
                    Console.Write("Enter your userID: ");
                    if (int.TryParse(Console.ReadLine(), out int userID))
                    {
                        await _createSubForumView.createSubForumAsync(title, description, userID);
                    }

                    break;
                case "4":
                    Console.Write("Enter Sub-Forum ID to Update: ");
                    if (int.TryParse(Console.ReadLine(), out int subForumIdToUpdate))
                    {
                        Console.Write("Enter New Sub-Forum Title: ");
                        var newTitle = Console.ReadLine();
                        Console.Write("Enter New Sub-Forum Description: ");
                        var newDescription = Console.ReadLine();
                        await _manageSubForumsView.UpdateSubForumAsync(subForumIdToUpdate, newTitle, newDescription);
                    }

                    break;
                case "5":
                    Console.Write("Enter Sub-Forum ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int subForumIdToDelete))
                    {
                        await _manageSubForumsView.DeleteSubForumAsync(subForumIdToDelete);
                    }

                    break;
                case "0":
                    await StartAsync();
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }

        }
        
    }
}