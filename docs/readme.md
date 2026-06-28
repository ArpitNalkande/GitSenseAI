# GitSense AI - GitHub Engineering Intelligence Platform

## Overview

GitSense AI is an AI-powered platform that analyzes GitHub repositories and provides engineering insights, health scoring, recommendations, and repository-aware chat capabilities.

The application combines:

- GitHub APIs
- Large Language Models (Gemini)
- Retrieval Augmented Generation (RAG)
- Semantic Search
- Custom Embeddings

to help developers understand repository activity and engineering quality.

## Features

- Repository Analysis
- AI Generated Repository Summary
- Repository Health Score
- Engineering Recommendations
- AI Chat with Repository Context
- Semantic Search
- Retrieval Augmented Generation (RAG)

## Target Users

- Software Engineers
- Engineering Managers
- Technical Leads
- Open Source Contributors
- Recruiters and Evaluators

# System Architecture

Frontend
↓
React + Vite + Tailwind CSS

Backend
↓
.NET 10 Minimal API

External Services
↓
GitHub REST API
Google Gemini API

AI Layer
↓
Repository Summary Service
Embedding Service
Semantic Search Service
RAG Chat Service



Data Layer
↓
SQL Server (future storage)

React UI
    ↓
.NET Minimal API
    ↓
GitHub API
    ↓
AI Services
        ├── Repository Summary
        ├── Embeddings
        ├── Semantic Search
        └── RAG Chat
    ↓
Gemini


# Repository Summary Workflow

User
↓
Enter Repository
↓
Backend calls GitHub APIs
↓
Build Prompt
↓
Gemini
↓
Summary + Health Score + Recommendations

---

# Chat Workflow (RAG)

Question
↓
Build Search Documents
↓
Generate Embeddings
↓
Semantic Search
↓
Retrieve Top Documents
↓
Build Context Prompt
↓
Gemini
↓
Answer + Sources


# AI Concepts Used

# Embeddings

Embeddings convert text into numerical vectors.

Example:

"Added JWT Authentication"

↓

[0.12, -0.55, 0.87 ...]

Embeddings allow machines to understand semantic meaning instead of exact keywords.

---

# Semantic Search

Semantic Search finds documents by meaning rather than exact text matching.

Example:

Question:
"How is login security implemented?"

Result:
"Added JWT Authentication"

---

# Cosine Similarity

Measures the similarity between two vectors.

1.0 → identical
0.8 → highly similar
0 → unrelated

---

# Retrieval Augmented Generation (RAG)

RAG consists of:

1. Retrieval
2. Augmentation
3. Generation

Question
↓
Retrieve Relevant Documents
↓
Provide Context to LLM
↓
Generate Final Answer


# How RAG is implemented

# Step 1

Convert repository commits, issues and pull requests into SearchDocuments.

# Step 2

Generate embeddings.

# Step 3

Perform semantic search using cosine similarity.

# Step 4

Retrieve top matching documents.

# Step 5

Build prompt with retrieved context.

# Step 6

Send prompt to Gemini.

# Step 7

Return answer and sources.


# Tech-Stack 

| Technology           | Usage                |
| -------------------- | -------------------- |
| .NET 10 Minimal API  | Backend              |
| C#                   | Business Logic       |
| React                | Frontend             |
| Vite                 | React Build Tool     |
| Tailwind CSS         | Styling              |
| SQL Server           | Future Persistence   |
| GitHub REST API      | Repository Data      |
| Gemini API           | AI Generation        |
| Axios                | API Communication    |
| Dependency Injection | Service Registration |
| HttpClientFactory    | External API Calls   |

