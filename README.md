# RAG Knowledge Agent

A .NET-based Retrieval-Augmented Generation (RAG) knowledge agent that answers questions using indexed knowledge sources.

## TL;DR

This project demonstrates a custom RAG pipeline built with:

- .NET Web API
- Azure AI Search for knowledge retrieval
- Azure OpenAI for grounded answer generation
- OpenAPI + Scalar for local API testing

The API accepts a natural language question, retrieves relevant document chunks from Azure AI Search, sends the retrieved context to Azure OpenAI, and returns a grounded answer with source references.

---

## Problem

Teams often have useful knowledge spread across runbooks, wikis, support notes, and troubleshooting guides. Searching manually is slow, especially as part of production incident response.

This project explores building a knowledge agent that answers questions using indexed source documents.

---

## Solution

The app follows a custom RAG flow:

```text
User
  ↓
POST /ask
  ↓
.NET API
  ↓
Azure AI Search retrieves relevant chunks
  ↓
Azure OpenAI generates a grounded answer
  ↓
Answer + sources returned to the user
```

The project does not rely on a pre-built Foundry Agent. The retrieval and answer-generation flow is orchestrated in code to demonstrate backend engineering and RAG architecture.

---

## Tech Stack

- .NET 10
- C#
- ASP.NET Core Minimal API
- Azure AI Search
- Azure OpenAI
- OpenAPI
- Scalar

---

## Architecture

```text
src/
├── RagKnowledgeAgent.Api
│   └── HTTP endpoint, OpenAPI/Scalar, dependency injection
├── RagKnowledgeAgent.Application
│   └── Interfaces, request/response models, orchestration logic
└── RagKnowledgeAgent.Infrastructure
    └── Azure AI Search retrieval and Azure OpenAI generation
```

---

## API Example

### `POST /ask`

Request:

```json
{
  "question": "FSS has a spike alert, what should I do?"
}
```

Response:

```json
{
  "answer": "Start by checking the spike alert runbook, reviewing the affected service and action, checking recent error patterns in logs, and validating whether the spike is caused by a known issue or recent deployment.",
  "sources": [
    "[New] Spike-Alerts-per-Service-Triage-Runbook.md",
    "Sumo-&-PagerDuty-Alert-Registry.md"
  ],
  "status": "AnsweredFromRetrievedKnowledge"
}
```

---

## Run Locally

### Prerequisites

- .NET 10 SDK
- Azure AI Search index
- Azure OpenAI deployment
- VS Code or Visual Studio

### Set local secrets

From the API project folder:

```powershell
cd src/RagKnowledgeAgent.Api
dotnet user-secrets init
```

Set Azure AI Search values:

```powershell
dotnet user-secrets set "AzureAiSearch:Endpoint" "https://YOUR-SEARCH-SERVICE.search.windows.net"
dotnet user-secrets set "AzureAiSearch:IndexName" "YOUR-INDEX-NAME"
dotnet user-secrets set "AzureAiSearch:ApiKey" "YOUR-QUERY-KEY"
```

Set Azure OpenAI values:

```powershell
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR-AZURE-OPENAI-RESOURCE.openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR-AZURE-OPENAI-KEY"
dotnet user-secrets set "AzureOpenAI:DeploymentName" "YOUR-DEPLOYMENT-NAME"
```

### Start the API

From the repository root:

```powershell
cd src/RagKnowledgeAgent.Api
dotnet run
```

Open Scalar API Reference:

```text
http://localhost:5004/scalar/v1
```

Test:

```http
POST /ask
```

---

## Azure AI Search Index Fields

The current implementation expects an index with fields similar to:

| Field | Purpose |
|---|---|
| `chunk` | Retrieved text/context |
| `title` | Source title |
| `parent_id` | Fallback source identifier |
| `chunk_id` | Fallback chunk identifier |
| `text_vector` | Vector field for future hybrid/vector search |
