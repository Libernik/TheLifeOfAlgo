using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Config")]
public class LevelConfig : ScriptableObject
{
    public string levelName;

    public LevelType levelType;
    public Difficulty difficulty;

    public Level levelPrefab;

    [Header("Generation")]
    public int itemCount = 3;

    [Header("Gameplay")]
    public int rewardXP = 1;
    public int livesPenalty = 1;

    [Header("Hash Rule")]
    public HashRuleType hashRuleType;

    [Header("Hash")]
    public bool shufflePackages = true;

    [Header("Portal")]
    public float timeLimit = 30f;

    [Header("Maze")]
    public int width = 10;
    public int height = 10;

    [Header("Hard UI")]
    [TextArea]
    public string taskDescription;

    public Sprite taskImage;

    [TextArea]
    public string inputDataToCopy;
}