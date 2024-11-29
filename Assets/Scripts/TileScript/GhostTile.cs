using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTile : TileModule
{
    [SerializeField] private float visibleDuration = 3f;
    [SerializeField] private float disappearDuration = 0.5f;
    
    protected override IEnumerator LoopBehavior()
    {
        while (isOn)
        {
            SetTileActive(true);
            yield return new WaitForSeconds(visibleDuration);
            
            SetTileActive(false);
            yield return new WaitForSeconds(disappearDuration);
        }
        yield return null;
    }
}
