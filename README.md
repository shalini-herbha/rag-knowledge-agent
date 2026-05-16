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
