using UnityEngine;

public static class HashRuleFactory
{
    public static IHashRule Create(HashRuleType type)
    {
        switch (type)
        {
            case HashRuleType.Easy:
                return new HashRuleEasy();

            case HashRuleType.Medium:
                return new HashRuleMedium();

            case HashRuleType.Hard:
                return new HashRuleHard();

            default:
                Debug.Log("Wrong HashRuleType in HashRuleFactory!");
                return new HashRuleEasy();
        }
    }
}
