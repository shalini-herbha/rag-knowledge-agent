namespace RagKnowledgeAgent.Application.Models;

public record AskQuestionResponse(
    string Answer,
    string[] Sources,
    string Status
);