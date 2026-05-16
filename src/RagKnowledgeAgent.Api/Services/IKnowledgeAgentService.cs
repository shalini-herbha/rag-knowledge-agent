using RagKnowledgeAgent.Api.Models;

namespace RagKnowledgeAgent.Api.Services;

public interface IKnowledgeAgentService
{
    Task<AskQuestionResponse> AskQuestionAsync(AskQuestionRequest request);
}