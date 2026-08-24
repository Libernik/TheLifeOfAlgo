using UnityEngine;
using System;

public abstract class Level : MonoBehaviour
{
    protected LevelConfig config;

    public event Action<LevelResult> OnLevelFinished;

    public event Action OnRegenerated;

    public virtual void InitWithConfig(LevelConfig cfg)
    {
        config = cfg;
        Init();
    }

    protected abstract void Init();

    protected abstract bool CheckAnswer(string answer);

    public abstract void Regenerate();

    protected void Finish(LevelResult result)
    {
        OnLevelFinished?.Invoke(result);
    }

    public virtual void SubmitAnswer(string answer)
    {
        if (CheckAnswer(answer) == true)
        {
            Finish(LevelResult.Solved);
        }
        else
        {
            Finish(LevelResult.Failed);
        }
    }

    public virtual void Deactivate()
    {

    }

    protected void NotifyRegenerated()
    {
        OnRegenerated?.Invoke();
    }
}