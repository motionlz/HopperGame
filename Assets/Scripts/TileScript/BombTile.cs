using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BombTile : TileModule
{
    protected override void TriggerModule()
    {
        int _index = GameManager.Instance.PlatformGenerator.GetTileIndex(this.gameObject);
        GameManager.Instance.PlatformGenerator.DisablePlatformAt(_index + 1);
        GameManager.Instance.PlatformGenerator.DisablePlatformAt(_index - 1);
        SetTileActive(false);
    }
}
