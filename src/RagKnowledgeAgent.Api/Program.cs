using RagKnowledgeAgent.Api.Models;
using RagKnowledgeAgent.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddScoped<IKnowledgeAgentService, KnowledgeAgentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapPost("/ask", (AskQuestionRequest request, IKnowledgeAgentService knowledgeAgentService) =>
{
    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new
        {
            error = "Question is required."
        });
    }

    var response = knowledgeAgentService.AskQuestion(request);

    return Results.Ok(response);
});

app.Run();