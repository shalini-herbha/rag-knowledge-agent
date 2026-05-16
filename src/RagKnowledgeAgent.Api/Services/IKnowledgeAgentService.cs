using RagKnowledgeAgent.Api.Models;

namespace RagKnowledgeAgent.Api.Services;

public interface IKnowledgeAgentService
{
    AskQuestionResponse AskQuestion(AskQuestionRequest request);
}