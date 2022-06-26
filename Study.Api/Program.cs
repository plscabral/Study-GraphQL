using Study.Api.Model;
using Study.Domain.Interfaces;
using Study.Repository.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Definindo as interfaces e repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.MapGraphQL("/");

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
