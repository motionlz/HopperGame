using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : CollectableObject
{
    public override void OnInteract()
    {
        GameManager.Instance.ScoreManager.AddScore();
        base.OnInteract();
    }
}
