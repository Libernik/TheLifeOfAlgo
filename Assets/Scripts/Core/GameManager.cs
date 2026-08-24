using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LevelManager levelManager;

    [SerializeField] private LevelDatabase database;

    private int currentLevelIndex;

    private int lives = 3;

    private int experience;

    public UIManager ui;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ui.HideGameOverScreen();
        ui.UpdateHearts(lives);
        ui.UpdateXP(experience);
        StartCurrentLevel();
    }

    void StartCurrentLevel()
    {
        var config = database.levels[currentLevelIndex];

        var level = levelManager.LoadLevel(config);

        if (config.difficulty == Difficulty.Hard)
        {
            if (level is IHardTaskProvider)
            {
                ui.InitHardLevelUI(level);
                ui.ShowHardLevelUI();
            }
        }
        else
        {
            ui.HideHardLevelUI();
        }
    }

    public void OnLevelSolved()
    {
        experience += database.levels[currentLevelIndex].rewardXP;

        ui.UpdateXP(experience);

        currentLevelIndex++;

        if (currentLevelIndex >= database.levels.Count)
        {
            Debug.Log("GAME COMPLETED!");
            ui.ShowGameOverScreen(experience, CountMaxXP(), true);
            return;
        }

        StartCurrentLevel();
    }

    public bool OnLevelFailed()
    {
        lives -= database.levels[currentLevelIndex].livesPenalty;

        ui.UpdateHearts(lives);

        if (lives <= 0)
        {
            Debug.Log("GAME OVER!");
            ui.ShowGameOverScreen(experience, CountMaxXP(), false);
            return false;
        }
        else
        {
            var config = database.levels[currentLevelIndex];
            if (config.difficulty == Difficulty.Hard)
            {
                    ui.ShowHardLevelUI();
            }
            return true;
        }
    }

    public void OnLevelSkipped()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= database.levels.Count)
        {
            Debug.Log("GAME COMPLETED!");
            ui.ShowGameOverScreen(experience, CountMaxXP(), true);
            return;
        }

        StartCurrentLevel();
    }

    void RestartGame()
    {
        lives = 3;
        experience = 0;
        currentLevelIndex = 0;
        Start(); 
    }

    void OnRestartButtonClick()
    {
        RestartGame();
    }

    int CountMaxXP()
    {
        int maxXP = 0;

        foreach(var level in database.levels)
        {
            maxXP += level.rewardXP;
        }

        return maxXP;
    }
}