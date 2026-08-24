using UnityEngine;

public interface ILevelGenerator<T>
{
    T Generate(LevelConfig config);
}