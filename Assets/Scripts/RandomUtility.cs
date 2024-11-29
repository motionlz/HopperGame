using UnityEngine;

public static class RandomUtility
{
    public static bool RandomPercentagePass(float _chance)
    {
        return Random.Range(0f,100f) < _chance;
    }
}
