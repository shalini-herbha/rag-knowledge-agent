using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services for API documentation.
builder.Services.AddOpenApi();

var app = builder.Build();

// Expose OpenAPI document and Scalar UI only in local/development environment.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

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

public record AskQuestionRequest(string Question);

public record AskQuestionResponse(string Answer, string[] Sources);