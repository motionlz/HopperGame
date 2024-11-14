using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerManager PlayerManager;
    public PlatformGenerator PlatformGenerator;
    
    protected override async void InitAfterAwake()
    {
        await Task.Delay(1);
        PlayerManager.Init();
        PlatformGenerator.Init();
    }
}
