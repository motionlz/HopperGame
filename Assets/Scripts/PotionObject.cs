using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionObject : CollectableObject
{
    public override void OnInteract()
    {
        GameManager.Instance.PlayerManager.SetFullHealth();
        base.OnInteract();
    }
}
