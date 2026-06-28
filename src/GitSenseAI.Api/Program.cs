using GitSenseAI.Api.Features.AI.RepositorySummary;
using GitSenseAI.Api.Features.Chat;
using GitSenseAI.Api.Features.Chat.Services;
using GitSenseAI.Api.Features.Repository.AnalyzeRepository;
using GitSenseAI.Api.Features.Repository.RepositoryDetails;
using GitSenseAI.Infrastructure.AI;
using GitSenseAI.Infrastructure.Configurations;
using GitSenseAI.Infrastructure.Github;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.Configure<GithubOptions>(
    builder.Configuration.GetSection(
        GithubOptions.SectionName));

builder.Services.AddScoped<IGithubRepositoryService,GithubRepositoryService>();

builder.Services.Configure<GeminiOptions>(builder.Configuration.GetSection(GeminiOptions.SectionName));

builder.Services.AddScoped<IAIService,GeminiAIService>();

builder.Services.AddScoped<IEmbeddingService, SimpleEmbeddingService>();

builder.Services.AddScoped<IRetrievalService,SemanticRetrievalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapAnalyzeRepositoryEndpoint();
app.MapRepositoryDetailsEndpoint();
app.MapRepositorySummaryEndpoint();
app.MapChatEndpoint();

app.Run();