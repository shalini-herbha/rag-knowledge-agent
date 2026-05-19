using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using RagKnowledgeAgent.Infrastructure.Options;
using RagKnowledgeAgent.Application.Services;

namespace RagKnowledgeAgent.Infrastructure.Generation;

public class AzureOpenAiAnswerGenerationService : IAnswerGenerationService
{
    private readonly ChatClient _chatClient;

    public AzureOpenAiAnswerGenerationService(IOptions<AzureOpenAiOptions> options)
    {
        var azureOpenAiOptions = options.Value;

        if (string.IsNullOrWhiteSpace(azureOpenAiOptions.Endpoint))
        {
            throw new InvalidOperationException("Azure OpenAI endpoint is missing.");
        }

        if (string.IsNullOrWhiteSpace(azureOpenAiOptions.ApiKey))
        {
            throw new InvalidOperationException("Azure OpenAI API key is missing.");
        }

        if (string.IsNullOrWhiteSpace(azureOpenAiOptions.DeploymentName))
        {
            throw new InvalidOperationException("Azure OpenAI deployment name is missing.");
        }

        var azureClient = new AzureOpenAIClient(
            new Uri(azureOpenAiOptions.Endpoint),
            new AzureKeyCredential(azureOpenAiOptions.ApiKey));

        _chatClient = azureClient.GetChatClient(azureOpenAiOptions.DeploymentName);
    }

    public async Task<string> GenerateAnswerAsync(string question, string retrievedContext)
    {
        var systemMessage = """
        You are a helpful knowledge agent.

        Answer the user's question using only the retrieved context.
        If the answer is not in the retrieved context, say:
        "I could not find enough information in the indexed knowledge sources to answer this."

        Keep the answer concise, practical, and step-by-step where useful.
        Do not invent details.
        """;

        var userMessage = $"""
        User question:
        {question}

        Retrieved context:
        {retrievedContext}
        """;

        var completion = await _chatClient.CompleteChatAsync(
        [
            new SystemChatMessage(systemMessage),
            new UserChatMessage(userMessage)
        ]);

        return completion.Value.Content[0].Text;
    }
}