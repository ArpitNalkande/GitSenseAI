function SummaryCard({
  summary,
}) {
  return (
    <div className="bg-white rounded-lg shadow p-6 mt-6">

      <h2 className="text-2xl font-bold mb-4">
        Repository Summary
      </h2>

      <p className="mb-6">
        {summary.summary}
      </p>

      <div className="mb-6">
        <span className="font-bold">
          Health Score:
        </span>

        <span className="ml-2 text-green-600 font-bold">
          {summary.healthScore}/100
        </span>
      </div>

      <h3 className="font-bold mb-2">
        Recommendations
      </h3>

      <ul className="list-disc ml-6">
        {summary.recommendations.map(
          (item, index) => (
            <li key={index}>
              {item}
            </li>
          )
        )}
      </ul>
    </div>
  );
}

export default SummaryCard;