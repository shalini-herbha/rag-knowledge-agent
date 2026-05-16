namespace RagKnowledgeAgent.Api.Services;

public class LocalFileKnowledgeRetrievalService : IKnowledgeRetrievalService
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileKnowledgeRetrievalService(IWebHostEnvironment environment)
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
                Sources: []
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