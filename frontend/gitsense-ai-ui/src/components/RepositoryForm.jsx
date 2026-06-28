function RepositoryForm({
  owner,
  repository,
  setOwner,
  setRepository,
  onAnalyze,
  loading,
}) {
  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-xl font-semibold mb-4">
        Analyze Repository
      </h2>

      <input
        className="w-full border p-3 rounded mb-4"
        placeholder="Repository Owner"
        value={owner}
        onChange={(e) =>
          setOwner(e.target.value)
        }
      />

      <input
        className="w-full border p-3 rounded mb-4"
        placeholder="Repository Name"
        value={repository}
        onChange={(e) =>
          setRepository(e.target.value)
        }
      />

      <button
        onClick={onAnalyze}
        disabled={loading}
        className="bg-slate-900 text-white px-5 py-3 rounded"
      >
        {loading
          ? "Analyzing..."
          : "Analyze Repository"}
      </button>
    </div>
  );
}

export default RepositoryForm;