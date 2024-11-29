using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTile : TileModule
{
    [SerializeField] private ChestObject chestObject;

    public override void ResetModule()
    {
        chestObject?.ResetObject();
        base.ResetModule();
    }
}
