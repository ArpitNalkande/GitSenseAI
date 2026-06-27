using GitSenseAI.Infrastructure.Configurations;
using GitSenseAI.Infrastructure.Github;
using GitSenseAI.Api.Features.Repository.AnalyzeRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.Configure<GithubOptions>(
    builder.Configuration.GetSection(
        GithubOptions.SectionName));

builder.Services.AddScoped<
    IGithubRepositoryService,
    GithubRepositoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapAnalyzeRepositoryEndpoint();

app.Run();