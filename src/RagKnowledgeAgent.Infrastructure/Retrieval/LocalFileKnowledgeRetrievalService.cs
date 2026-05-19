using Microsoft.Extensions.Hosting;
using RagKnowledgeAgent.Application.Services;

namespace RagKnowledgeAgent.Infrastructure.Retrieval;

public class LocalFileKnowledgeRetrievalService : IKnowledgeRetrievalService
{
    private readonly IHostEnvironment _environment;

    public LocalFileKnowledgeRetrievalService(IHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<RetrievedKnowledge> RetrieveAsync(string question)
    {
        const string relativePath = "SampleData/sample-runbook.md";

        var fullPath = Path.Combine(_environment.ContentRootPath, relativePath);

        if (!File.Exists(fullPath))
        {
            return new RetrievedKnowledge(
                Content: string.Empty,
                Sources: Array.Empty<string>()
            );
        }

        var content = await File.ReadAllTextAsync(fullPath);

        return new RetrievedKnowledge(
            Content: content,
            Sources:
            [
                relativePath
            ]
        );
    }
}