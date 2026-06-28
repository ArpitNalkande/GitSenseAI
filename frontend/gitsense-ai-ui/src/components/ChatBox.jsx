import { useState } from "react";
import api from "../services/api";
import SourcesList from "./SourcesList";

function ChatBox({
  owner,
  repository,
  chatResponse,
  setChatResponse,
}) {
  const [question, setQuestion] =
    useState("");

  const [loading, setLoading] =
    useState(false);

  const askQuestion =
    async () => {
      if (!question) {
        return;
      }

      try {
        setLoading(true);

        const response =
          await api.post(
            "/api/chat",
            {
              owner,
              repository,
              question,
            }
          );

        setChatResponse(
          response.data
        );
      }
      catch (error) {
        console.error(error);
      }
      finally {
        setLoading(false);
      }
    };

  return (
    <div className="bg-white rounded-lg shadow p-6 mt-6">

      <h2 className="text-2xl font-bold mb-4">
        Ask Repository
      </h2>

      <input
        className="w-full border p-3 rounded mb-4"
        placeholder="Ask a question..."
        value={question}
        onChange={(e) =>
          setQuestion(
            e.target.value
          )
        }
      />

      <button
        onClick={askQuestion}
        className="bg-slate-900 text-white px-5 py-3 rounded"
      >
        {loading
          ? "Thinking..."
          : "Ask"}
      </button>

      {chatResponse && (
        <>
          <div className="mt-6">
            <h3 className="font-bold mb-2">
              Answer
            </h3>

            <p>
              {
                chatResponse.answer
              }
            </p>
          </div>

          <SourcesList
            sources={
              chatResponse.sources
            }
          />
        </>
      )}
    </div>
  );
}

export default ChatBox;