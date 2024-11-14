using UnityEngine;

public class BrokeTile : TileModule
{
    protected override void TriggerModule()
    {
        this.gameObject.SetActive(false);
    }
}
