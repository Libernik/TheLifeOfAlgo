using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelConfig> levels;
}