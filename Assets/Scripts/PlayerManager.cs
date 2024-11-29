using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : UnitModule
{
    public PlayerAction PlayerActionModule { get; private set; }

    public void Init()
    {
        if(PlayerActionModule == null)
        {
            PlayerActionModule = GetComponent<PlayerAction>();
            PlayerActionModule.Init();
        }
        
        ResetUnit();
    }
    
    public override void TakeDamage(int _damage)
    {
        if (PlayerActionModule.IsBlock()) return;
        
        base.TakeDamage(_damage);
    }

    protected override void Dead()
    {
        PlayerActionModule.enabled = false;
        gameObject.SetActive(false);
        base.Dead();
    }
}
