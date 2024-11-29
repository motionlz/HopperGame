using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCoinTile : TileModule
{
    [SerializeField] private CoinObject coinObject;
    [SerializeField] private FallTile fallTile;

    public override void ResetModule()
    {
        coinObject.ResetModule();
        fallTile.ResetModule();
        fallTile.transform.rotation = Quaternion.identity;
        base.ResetModule();
    }
}
