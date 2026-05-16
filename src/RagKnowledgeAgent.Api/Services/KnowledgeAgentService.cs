using RagKnowledgeAgent.Api.Models;

namespace RagKnowledgeAgent.Api.Services;

public class KnowledgeAgentService : IKnowledgeAgentService
{
    public AskQuestionResponse AskQuestion(AskQuestionRequest request)
    {
        return new AskQuestionResponse(
            Answer: "Based on the sample runbook, you should first confirm the API is reachable, check whether the search service is returning errors, review recent deployment changes, and inspect logs for timeout or validation errors.",
            Sources:
            [
                "samples/documents/sample-runbook.md"
            ]
        );
    }
}