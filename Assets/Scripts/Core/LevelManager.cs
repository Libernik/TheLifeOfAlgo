using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Level currentLevel;

    public Level LoadLevel(LevelConfig config)
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(config.levelPrefab);

        currentLevel.InitWithConfig(config);

        currentLevel.transform.SetParent(transform);

        currentLevel.OnLevelFinished += HandleLevelFinished;

        return currentLevel;
    }

    void HandleLevelFinished(LevelResult result)
    {
        switch(result)
        {
            case LevelResult.Solved:
                Debug.Log("Level Result: Solved");
                currentLevel.OnLevelFinished -= HandleLevelFinished;
                GameManager.Instance.OnLevelSolved();
                break;

            case LevelResult.Skipped:
                Debug.Log("Level Result: Skipped");
                currentLevel.OnLevelFinished -= HandleLevelFinished;
                GameManager.Instance.OnLevelSkipped();
                break;

            case LevelResult.Failed:
                Debug.Log("Level Result: Failed");
                if (GameManager.Instance.OnLevelFailed() == true)
                {
                    Debug.Log("LevelManager: Level.Regenerate");
                    currentLevel.Regenerate();
                }
                else
                {
                    currentLevel.Deactivate();
                }
                break;

            default:
                break;
        }
    }
}