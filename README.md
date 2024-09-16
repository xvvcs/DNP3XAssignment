# CLI Forum Management Application

This is a Command Line Interface (CLI) application for managing a forum. The application allows users to manage posts, comments, users, moderators, and sub-forums.

## Features

- **Manage Posts**: Create, view, update, and delete posts.
- **Manage Comments**: Create, view, update, and delete comments.
- **Manage Users**: Create, view, update, and delete users.
- **Manage Moderators**: Create, view, update, and delete moderators.
- **Manage Sub-Forums**: Create, view, update, and delete sub-forums.

## Technologies Used

- **Language**: C#
- **Framework**: .NET
- **Repositories**: In-memory repositories for users, comments, posts, moderators, and sub-forums.

## Getting Started

### Prerequisites

- .NET SDK installed on your machine.

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/xvvcs/cli-forum-management.git
    cd cli-forum-management
    ```

2. Build the project:
    ```sh
    dotnet build
    ```

3. Run the application:
    ```sh
    dotnet run --project Server/CLI
    ```

## Project Structure

- `Server/CLI/Program.cs`: Entry point of the application.
- `Server/CLI/UI/CLIApp.cs`: Main application class handling the CLI menu and navigation.
- `Server/CLI/UI/ManageComments/ManageCommentsView.cs`: Handles comment management operations.
- `Server/InMemoryRepositories/UserInMemoryRepository.cs`: In-memory repository for user data.

## Usage

Upon running the application, you will be presented with a menu to manage different entities like posts, comments, users, moderators, and sub-forums. Follow the on-screen instructions to perform various operations.

### Example

To create a new post:
1. Select "Manage Posts" from the main menu.
2. Choose "Create Post".
3. Enter the required details like post title, body, and user ID.

## TODO

- Create dummy data for sub-forums and moderators.
- Implement log in and log out functionality.
- Fix likes and dislikes to prevent infinite liking/disliking.
- Ensure likes and dislikes are retained when updating comments and posts.


## Contributors

- [xvvcs](https://github.com/xvvcs)
- [Richardzik](https://github.com/Richardzik)
- [Cenone22](https://github.com/Cenone22)

## License

This project is licensed under the MIT License.

## Contact

For any questions or feedback, please contact [xvvcs](https://github.com/xvvcs).

Domain Model:
![Alt text](https://raw.githubusercontent.com/xvvcs/DNP3XAssignment1/master/DomainModelAssigment1.png)
