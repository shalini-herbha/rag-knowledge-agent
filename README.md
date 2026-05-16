# RAG Knowledge Agent

A portfolio project demonstrating a Retrieval-Augmented Generation (RAG) knowledge agent built with Azure AI Foundry, Azure AI Search, and .NET.

## Problem

Many teams have useful knowledge spread across documents, wikis, runbooks, and support notes. Searching manually is slow, and standard chatbots can give ungrounded or unreliable answers.

This project explores how to build an AI knowledge agent that can answer questions using indexed source documents.

## Solution

The agent uses a RAG-style architecture:

1. Documents are uploaded into a knowledge source.
2. Azure AI Search indexes the content.
3. The user asks a question.
4. The application retrieves relevant document chunks.
5. The LLM generates an answer grounded in the retrieved content.

## Planned Tech Stack

- .NET / C#
- Azure AI Foundry
- Azure OpenAI
- Azure AI Search
- Docker
- GitHub Actions

## Planned Features

- Ask questions over indexed documents
- Return grounded answers with source references
- Handle unsupported or unclear questions gracefully
- Basic API endpoint for asking questions
- Sample documents for local testing
- Simple frontend or API testing instructions

## Repository Structure

```text
rag-knowledge-agent/
├── docs/
│   ├── architecture.md
│   └── roadmap.md
├── samples/
│   └── documents/
│       └── sample-runbook.md
├── .gitignore
├── LICENSE
└── README.md
```

## Run Locally

### Prerequisites

- .NET 10 SDK
- VS Code or Visual Studio
- Git

### Start the API

From the repository root:

```powershell
cd src/RagKnowledgeAgent.Api
dotnet run
```

The API will start on a local port such as:

```text
http://localhost:5004
```

### Test the `/ask` endpoint

Open Scalar API Reference in the browser:

```text
http://localhost:5004/scalar/v1
```

Then test:

```http
POST /ask
```

Example request:

```json
{
  "question": "What should I check if search results are not loading?"
}
```

Example response:

```json
{
  "answer": "Based on the sample runbook, you should first confirm the API is reachable, check whether the search service is returning errors, review recent deployment changes, and inspect logs for timeout or validation errors.",
  "sources": [
    "samples/documents/sample-runbook.md"
  ]
}
```

### Current behaviour

The `/ask` endpoint currently returns a hardcoded response based on the sample runbook. Future versions will replace this with retrieval from Azure AI Search and grounded answer generation using Azure OpenAI / Azure AI Foundry.

## Documentation

- [Architecture](docs/architecture.md)
- [Roadmap](docs/roadmap.md)
- [Sample Runbook](samples/documents/sample-runbook.md)

## Architecture

```text
User
  ↓
.NET API
  ↓
Azure AI Search
  ↓
Azure OpenAI / Azure AI Foundry
  ↓
Grounded answer with sources
