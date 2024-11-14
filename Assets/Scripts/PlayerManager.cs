using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAction PlayerActionModule { get; private set; }
    public PlayerStatus PlayerStatusModule { get; private set; }

    public void Init()
    {
        if(PlayerActionModule == null)
        {
            PlayerActionModule = GetComponent<PlayerAction>();
            PlayerActionModule.Init();
            PlayerActionModule.AssignTo(this);
        }
        if(PlayerStatusModule == null)
        {
            PlayerStatusModule = GetComponent<PlayerStatus>();
            PlayerStatusModule.AssignTo(this);
        }
    }
}
