using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using AppContext = EfcRepositories.AppContext;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repositories to the container
builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
builder.Services.AddScoped<ICommentRepository, EfcCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfcUserRepository>();
builder.Services.AddScoped<IModeratorRepository, ModeratorFileRepository>();
builder.Services.AddScoped<ISubForumRepository, SubForumFileRepository>();
builder.Services.AddDbContext<AppContext>();

var app = builder.Build();
app.MapControllers();

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