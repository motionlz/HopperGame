using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : TileModule
{
    [Header("Trap Settings")]
    [SerializeField] private GameObject spearObject;
    [SerializeField] private GameObject warningIndicator;
    [SerializeField] private float hideDuration = 2.5f;
    [SerializeField] private float warningDuration = 0.5f;
    [SerializeField] private float strikeDuration = 0.5f;
    
    protected override void StopLoop()
    {
        base.StopLoop();
        
        SetSpearActive(false);
        SetWarningActive(false);
    }
    protected override IEnumerator LoopBehavior()
    {
        while (isOn)
        {
            SetSpearActive(false);
            yield return new WaitForSeconds(hideDuration);
            
            SetWarningActive(true);
            yield return new WaitForSeconds(warningDuration);
            
            SetWarningActive(false);
            SetSpearActive(true);
            yield return new WaitForSeconds(strikeDuration);
        }
    }
    
    private void SetSpearActive(bool _isActive)
    {
        if (!spearObject) return;
        spearObject.SetActive(_isActive);
    }
    private void SetWarningActive(bool _isActive)
    {
        if (!warningIndicator) return;
        warningIndicator.SetActive(_isActive);
    }
}
