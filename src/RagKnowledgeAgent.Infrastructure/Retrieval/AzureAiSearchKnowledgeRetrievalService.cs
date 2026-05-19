using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using RagKnowledgeAgent.Application.Services;
using RagKnowledgeAgent.Infrastructure.Options;

namespace RagKnowledgeAgent.Infrastructure.Retrieval;

public class AzureAiSearchKnowledgeRetrievalService : IKnowledgeRetrievalService
{
    private readonly SearchClient _searchClient;

    public AzureAiSearchKnowledgeRetrievalService(IOptions<AzureAiSearchOptions> options)
    {
        var searchOptions = options.Value;

        if (string.IsNullOrWhiteSpace(searchOptions.Endpoint))
        {
            throw new InvalidOperationException("Azure AI Search endpoint is missing.");
        }

        if (string.IsNullOrWhiteSpace(searchOptions.IndexName))
        {
            throw new InvalidOperationException("Azure AI Search index name is missing.");
        }

        if (string.IsNullOrWhiteSpace(searchOptions.ApiKey))
        {
            throw new InvalidOperationException("Azure AI Search API key is missing.");
        }

        _searchClient = new SearchClient(
            new Uri(searchOptions.Endpoint),
            searchOptions.IndexName,
            new AzureKeyCredential(searchOptions.ApiKey)
        );
    }

    public async Task<RetrievedKnowledge> RetrieveAsync(string question)
    {
        if (string.IsNullOrWhiteSpace(question))
        {
            return new RetrievedKnowledge(
                Content: string.Empty,
                Sources: []
            );
        }

        var searchOptions = new SearchOptions
        {
            Size = 3
        };

        var response = await _searchClient.SearchAsync<SearchDocument>(
            question,
            searchOptions
        );

        var matchedContent = new List<string>();
        var matchedSources = new List<string>();

        await foreach (var result in response.Value.GetResultsAsync())
        {
            var document = result.Document;

            var chunk = GetStringValue(document, "chunk");
            var source = GetSourceName(document);

            if (!string.IsNullOrWhiteSpace(chunk))
            {
                matchedContent.Add(chunk);
            }

            if (!string.IsNullOrWhiteSpace(source))
            {
                matchedSources.Add(source);
            }
        }

        return new RetrievedKnowledge(
            Content: string.Join(Environment.NewLine + Environment.NewLine, matchedContent),
            Sources: matchedSources
                .Where(source => !string.IsNullOrWhiteSpace(source))
                .Distinct()
                .ToArray()
        );
    }

    private static string GetSourceName(SearchDocument document)
    {
        var title = GetStringValue(document, "title");

        if (!string.IsNullOrWhiteSpace(title))
        {
            return title;
        }

        var parentId = GetStringValue(document, "parent_id");

        if (!string.IsNullOrWhiteSpace(parentId))
        {
            return parentId;
        }

        var chunkId = GetStringValue(document, "chunk_id");

        if (!string.IsNullOrWhiteSpace(chunkId))
        {
            return chunkId;
        }

        return "Unknown source";
    }

    private static string GetStringValue(SearchDocument document, string fieldName)
    {
        if (!document.TryGetValue(fieldName, out var value))
        {
            return string.Empty;
        }

        return value?.ToString() ?? string.Empty;
    }
}