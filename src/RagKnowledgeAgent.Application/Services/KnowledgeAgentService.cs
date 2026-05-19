using RagKnowledgeAgent.Application.Models;

namespace RagKnowledgeAgent.Application.Services;

public class KnowledgeAgentService : IKnowledgeAgentService
{
    private readonly IKnowledgeRetrievalService _knowledgeRetrievalService;
    private readonly IAnswerGenerationService _answerGenerationService;

    public KnowledgeAgentService(
        IKnowledgeRetrievalService knowledgeRetrievalService,
        IAnswerGenerationService answerGenerationService)
    {
        _knowledgeRetrievalService = knowledgeRetrievalService;
        _answerGenerationService = answerGenerationService;
    }

    public async Task<AskQuestionResponse> AskQuestionAsync(AskQuestionRequest request)
    {
        var retrievedKnowledge = await _knowledgeRetrievalService.RetrieveAsync(request.Question);

        if (string.IsNullOrWhiteSpace(retrievedKnowledge.Content))
        {
            return new AskQuestionResponse(
                Answer: "I could not find any relevant knowledge source to answer this question.",
                Sources: [],
                Status: "NoRelevantKnowledgeFound"
            );
        }

        var answer = await _answerGenerationService.GenerateAnswerAsync(
            request.Question,
            retrievedKnowledge.Content);

        return new AskQuestionResponse(
            Answer: answer,
            Sources: retrievedKnowledge.Sources,
            Status: "AnsweredFromRetrievedKnowledge"
        );
    }
}