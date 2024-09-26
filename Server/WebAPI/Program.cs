using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

//TODO: ADD BUSINESS LOGIC TO THE APPLICATION
//TODO: ADD VALIDATION FOR CREATING USERS
//TODO: FIX LIKES AND DISLIKES

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repositories to the container
builder.Services.AddScoped<IPostRepository, PostFileRepository>();
builder.Services.AddScoped<ICommentRepository, CommentFileRepository>();
builder.Services.AddScoped<IUserRepository, UserFileRepository>();
builder.Services.AddScoped<IModeratorRepository, ModeratorFileRepository>();
builder.Services.AddScoped<ISubForumRepository, SubForumFileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();