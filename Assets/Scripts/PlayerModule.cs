using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModule : MonoBehaviour
{
    protected PlayerManager PlayerManager { get; private set; }

    public void AssignTo(PlayerManager target) 
    {
        PlayerManager = target;
    }
}
