using RagKnowledgeAgent.Application.Models;

namespace RagKnowledgeAgent.Application.Services;

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
                Sources: Array.Empty<string>(),
                Status: "NoRelevantKnowledgeFound"
            );
        }

        var answer = BuildAnswerFromRetrievedKnowledge(retrievedKnowledge.Content);

        return new AskQuestionResponse(
            Answer: answer,
            Sources: retrievedKnowledge.Sources,
            Status: "AnsweredFromRetrievedKnowledge"
        );
    }

    private static string BuildAnswerFromRetrievedKnowledge(string content)
    {
        if (content.Contains("Initial Checks", StringComparison.OrdinalIgnoreCase))
        {
            return "Based on the sample runbook, start by confirming the API is reachable, checking whether the search service is returning errors, reviewing recent deployment changes, and inspecting logs for timeout or validation errors.";
        }

        return "I found a relevant knowledge source, but could not extract a specific answer from it yet.";
    }
}