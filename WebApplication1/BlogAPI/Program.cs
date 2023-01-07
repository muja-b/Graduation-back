using WebApplication1.Middlewares;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient <Authentication> ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<modelContext>();
builder.Services.AddScoped<IPostsService,PostsService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<Authentication>();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
