using RagKnowledgeAgent.Api.Models;

namespace RagKnowledgeAgent.Api.Services;

public class KnowledgeAgentService : IKnowledgeAgentService
{
    private readonly IKnowledgeRetrievalService _knowledgeRetrievalService;

    public KnowledgeAgentService(IKnowledgeRetrievalService knowledgeRetrievalService)
    {
        _knowledgeRetrievalService = knowledgeRetrievalService;
    }

    public async Task<AskQuestionResponse> AskQuestionAsync(AskQuestionRequest request)
    {
        var retrievedKnowledge = await _knowledgeRetrievalService.RetrieveAsync(request.Question);

        if (string.IsNullOrWhiteSpace(retrievedKnowledge.Content))
        {
            return new AskQuestionResponse(
                Answer: "I could not find any relevant knowledge source to answer this question.",
                Sources: []
            );
        }

        return new AskQuestionResponse(
            Answer: "Based on the sample runbook, you should first confirm the API is reachable, check whether the search service is returning errors, review recent deployment changes, and inspect logs for timeout or validation errors.",
            Sources: retrievedKnowledge.Sources
        );
    }
}