using UnityEngine;

public class PortalLevel : Level
{
    [SerializeField] private PortalLevelView view;

    private PlayerMovement player;

    private PortalLevelData data;

    private PortalLevelGenerator generator = new();

    public LevelTimer timer;

    protected override void Init()
    { 
        Generate();
    }

    void Generate()
    {
        data = generator.Generate(config);
        data.FindShortestPath();

        timer.SetTimer(data.shortestPath.totalCost + data.shortestPath.islandsCount);

        player = view.Build(data);

        player.StopMovement();

        view.FinishReached += OnFinishReached;

        timer.OnTimeOver += OnTimeOver;
    }

    public void StartTimer()
    {
        timer.StartTimer();
        player.StartMovement();
        view.startButton.gameObject.SetActive(false);
    }

 
    public override void Regenerate()
    {
        Deactivate();

        Generate();
    }

    protected override bool CheckAnswer(string answer)
    {
        if (answer == "finish") return true;
        return false;
    }

    public override void Deactivate()
    {
        player.StopMovement();

        view.FinishReached -= OnFinishReached;

        timer.OnTimeOver -= OnTimeOver;
    }

    private void OnFinishReached()
    {
        SubmitAnswer("finish");
    }

    private void OnTimeOver()
    {
        SubmitAnswer("");
    }
}