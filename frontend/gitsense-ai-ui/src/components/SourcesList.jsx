function SourcesList({
  sources,
}) {
  return (
    <div className="mt-6">
      <h3 className="font-bold mb-2">
        Sources
      </h3>

      {sources.map(
        (source) => (
          <div
            key={source.id}
            className="border rounded p-3 mb-2"
          >
            <div className="font-semibold">
              {source.type}
            </div>

            <div>
              {source.content}
            </div>
          </div>
        )
      )}
    </div>
  );
}

export default SourcesList;