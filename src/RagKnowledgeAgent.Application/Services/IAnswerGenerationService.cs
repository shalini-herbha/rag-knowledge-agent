namespace RagKnowledgeAgent.Application.Services;

public interface IAnswerGenerationService
{
    Task<string> GenerateAnswerAsync(string question, string retrievedContext);
}