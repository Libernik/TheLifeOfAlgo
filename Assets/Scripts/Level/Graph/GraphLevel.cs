using UnityEngine;

public class GraphLevel : Level
{
    [SerializeField] private MatrixView matrixView;

    [SerializeField] private GraphView graphView;

    private GraphGenerator generator;

    private GraphLevelData data;

    private ILevelObjective objective;

    void Awake()
    {
        generator = new GraphGenerator();
    }

    protected override void Init()
    {
        data = generator.Generate(config);

        matrixView.Build(data.graph.nodeCount);

        graphView.Build(data.graph);

        objective =
            new GraphObjective(
                data.graph,
                matrixView);
    }

    protected override bool CheckAnswer(string answer)
    {
        return objective.Check();
    }

    public override void Regenerate()
    {
        Init();
    }

    public void OnSubmitButton()
    {
        SubmitAnswer("");
    }
}