# Architecture

This document will describe the high-level architecture of the RAG Knowledge Agent.

## Planned Flow

1. User asks a question.
2. .NET API receives the request.
3. Relevant document chunks are retrieved from Azure AI Search.
4. Azure OpenAI generates a grounded answer.
5. The response is returned with source references.

## Architecture Diagram

```text
User
  ↓
.NET API
  ↓
Azure AI Search
  ↓
Azure OpenAI / Azure AI Foundry
  ↓
Answer with sources
