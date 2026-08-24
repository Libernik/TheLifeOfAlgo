using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MazeLevel : Level, IHardTaskProvider, ISkippableLevel
{
    [SerializeField] private MazeLevelView view;

    [SerializeField] private LevelTimer timer;

    private MazeGenerator generator = new();

    private MazeData maze;

    private PlayerMovement player;

    private bool taskIsSolved;

    protected override void Init()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    { 
        maze = generator.Generate(config);

        taskIsSolved = false;

        timer.SetTimer(maze.path.pathLength / 2);

        EnableMazeCamera();

        view.FinishReached += OnFinishReached;

        timer.OnTimeOver += OnTimeOver;

        player = view.Build(maze);
    }

    public override void Regenerate()
    {
        Deactivate();

        GenerateLevel();

        NotifyRegenerated();
    }

    protected override bool CheckAnswer(string answer)
    {
        if (answer == "finish" || answer == maze.hardTask.ExpectedAnswer) return true;
        return false;
    }

    public override void SubmitAnswer(string answer)
    {
        Debug.Log(answer);
        if (CheckAnswer(answer) == true)
        {
            if(taskIsSolved == true)
            {
                Deactivate();
                Finish(LevelResult.Solved);
            }
            else
            {
                taskIsSolved = true;
                GameManager.Instance.ui.HideHardLevelUI();
            }
        }
        else
        {
            Finish(LevelResult.Failed);
        }
    }

    void StartTimer()
    {
        timer.StartTimer();
        player.StartMovement();
        view.startButton.gameObject.SetActive(false);
    }

    public override void Deactivate()
    {
        player.StopMovement();

        player.DisableTorch();

        DisableMazeCamera();

        view.FinishReached -= OnFinishReached;

        timer.OnTimeOver -= OnTimeOver;
    }

    void EnableMazeCamera()
    {
        Camera cam = Camera.main;

        CameraFollow follow =
            cam.GetComponent<CameraFollow>();

        follow.enabled = true;

        cam.orthographicSize = 2.5f;
    }

    void DisableMazeCamera()
    {
        Camera cam = Camera.main;

        CameraFollow follow = cam.GetComponent<CameraFollow>();

        follow.enabled = false;

        cam.orthographicSize = 10;

        cam.transform.position = new Vector3(0f, 0f, -10f);
    }

    public HardTask GetHardTask()
    {
        return maze.hardTask;
    }

    public void SkipLevel()
    {
        Deactivate();
        Finish(LevelResult.Skipped);
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