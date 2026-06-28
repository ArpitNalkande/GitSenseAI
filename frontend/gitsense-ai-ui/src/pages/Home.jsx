import { useState } from "react";
import Header from "../components/Header";
import RepositoryForm from "../components/RepositoryForm";
import SummaryCard from "../components/SummaryCard";
import ChatBox from "../components/ChatBox";
import api from "../services/api";

function Home() {
  const [owner, setOwner] = useState("");
  const [repository, setRepository] = useState("");
  const [summary, setSummary] = useState(null);
  const [loading, setLoading] = useState(false);
  const [chatResponse, setChatResponse] = useState(null);

  const analyzeRepository = async () => {
    try {
      setLoading(true);

      const response = await api.post(
        "/api/ai/repository-summary",
        {
          owner,
          repository,
        }
      );

      setSummary(response.data);
    }
    catch (error) {
      console.error(error);
      alert(
        "Unable to analyze repository."
      );
    }
    finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Header />

      <div className="max-w-6xl mx-auto p-6">
        <RepositoryForm
          owner={owner}
          repository={repository}
          setOwner={setOwner}
          setRepository={setRepository}
          onAnalyze={analyzeRepository}
          loading={loading}
        />

        {summary && (
          <>
            <SummaryCard
              summary={summary}
            />

            <ChatBox
              owner={owner}
              repository={repository}
              chatResponse={chatResponse}
              setChatResponse={setChatResponse}
            />
          </>
        )}
      </div>
    </>
  );
}

export default Home;