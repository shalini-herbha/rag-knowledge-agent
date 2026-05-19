namespace RagKnowledgeAgent.Application.Services;

public interface IKnowledgeRetrievalService
{
    Task<RetrievedKnowledge> RetrieveAsync(string question);
}

public record RetrievedKnowledge(
    string Content,
    string[] Sources
);