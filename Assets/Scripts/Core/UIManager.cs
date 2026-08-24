using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HUDController hud;

    [SerializeField] private GameOverScreen gameOverScreen;

    [SerializeField] private HardLevelUI hardLevel;

    public void UpdateHearts(int lives)
    {
        hud.UpdateHearts(lives);
    }

    public void UpdateXP(int value)
    {
        hud.UpdateXP(value);
    }

    public void InitHardLevelUI(Level level)
    {
        hardLevel.Init(level);
    }

    public void ShowHardLevelUI()
    {
        hardLevel.Show();
    }

    public void HideHardLevelUI()
    {
        hardLevel.Hide();
    }

    public void ShowGameOverScreen(int xp, int maxXP, bool complete)
    {
        gameOverScreen.Show(xp, maxXP, complete);
    }

    public void HideGameOverScreen()
    {
        gameOverScreen.Hide();
    }
}
