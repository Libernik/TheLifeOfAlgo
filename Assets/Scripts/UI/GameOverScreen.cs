using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject root;

    [SerializeField] private TMP_Text gameOverText;

    public void Show(int xp, int maxXP, bool complete)
    {
        root.SetActive(true);
        string result = complete ? "GAME COMPLETED" : "GAME OVER";
        gameOverText.text = $"{result}! {xp} / {maxXP} XP";
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}