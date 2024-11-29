using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerManager PlayerManager;
    public PlatformGenerator PlatformGenerator;
    public LavaManager LavaManager;
    public FireBallManager FireBallManager;
    public ScoreManager ScoreManager;
    public UIManager UIManager;
    public Camera MainCamera;
    
    protected override async void InitAfterAwake()
    {
        await Task.Delay(1);
        PlayerManager.Init();
        PlatformGenerator.Init();
        LavaManager.Init();
        FireBallManager.Init();
        ScoreManager.Init();
        UIManager.Init();
    }
}
