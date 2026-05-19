using RagKnowledgeAgent.Application.Models;

namespace RagKnowledgeAgent.Application.Services;

public interface IKnowledgeAgentService
{
    Task<AskQuestionResponse> AskQuestionAsync(AskQuestionRequest request);
}