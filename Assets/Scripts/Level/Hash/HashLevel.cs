using UnityEngine;

public class HashLevel : Level
{
    [SerializeField] private HashLevelView view;

    private HashLevelGenerator generator;
    private HashLevelData data;
    private IHashRule rule;
    private ILevelObjective objective;

    void Awake()
    {
        generator = new HashLevelGenerator();
    }

    protected override void Init()
    {
        Generate();
    }

    void Generate()
    {
        rule = HashRuleFactory.Create(config.hashRuleType);

        data = generator.Generate(config);

        view.Build(data);

        objective = new HashMatchObjective(view.Packages, rule);
    }

    protected override bool CheckAnswer(string answer)
    {
        return objective.Check();
    }

    public override void Regenerate()
    {
        Generate();
    }

    public void OnSubmitButton()
    {
        SubmitAnswer("");
    }
}