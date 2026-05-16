using RagKnowledgeAgent.Api.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapPost("/ask", (AskQuestionRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Question))
    {
        return Results.BadRequest(new
        {
            error = "Question is required."
        });
    }

    var response = new AskQuestionResponse(
        Answer: "Based on the sample runbook, you should first confirm the API is reachable, check whether the search service is returning errors, review recent deployment changes, and inspect logs for timeout or validation errors.",
        Sources:
        [
            "samples/documents/sample-runbook.md"
        ]
    );

    return Results.Ok(response);
});

app.Run();