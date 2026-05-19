using RagKnowledgeAgent.Application.Models;
using RagKnowledgeAgent.Application.Services;
using RagKnowledgeAgent.Infrastructure.Retrieval;
using RagKnowledgeAgent.Infrastructure.Options;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<AzureAiSearchOptions>(builder.Configuration.GetSection("AzureAiSearch"));

builder.Services.AddScoped<IKnowledgeAgentService, KnowledgeAgentService>();
builder.Services.AddScoped<IKnowledgeRetrievalService, AzureAiSearchKnowledgeRetrievalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapPost("/ask", async (AskQuestionRequest request, IKnowledgeAgentService knowledgeAgentService) =>
{
    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new
        {
            error = "Question is required."
        });
    }

    var response = await knowledgeAgentService.AskQuestionAsync(request);

    return Results.Ok(response);
});

app.Run();