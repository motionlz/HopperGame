using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Platform Settings")] 
    [SerializeField]private int starterPlatformCount = 7;
    [SerializeField]private int starterPlatformStep = 4;
    [SerializeField]private float platformDifferenceHeight = 0.2f;
    [SerializeField]private float platformStartHeight = 0.2f;
    [SerializeField]private int maxStep = 8;
    [SerializeField]private int maxRandomStep = 3;
    [SerializeField]private int maxRetainCount = 5;
    [SerializeField]private int maxPlatformCount = 10;
    [SerializeField]private List<TileSpawnChance> commonTileChance = new List<TileSpawnChance>();

    [Header("Enemy Settings")] 
    [SerializeField]private float enemySpawnChance;
    
    
    private int retainCount;
    private int currentStep;
    private int nextStep;
    private int targetStep;
    private int currentPosition = 0;
    private List<GameObject> platformsList = new List<GameObject>();
    private List<String> commonTilePool = new List<String>();

    public void Init()
    {
        SetUpAllPool();
        GenerateStartPlatform();
        GameManager.Instance.PlayerManager.PlayerActionModule.OnJump += () => GenerateNextPlatform();
    }

    private void GenerateStartPlatform()
    {
        retainCount = starterPlatformCount;
        currentStep = starterPlatformStep;
        nextStep = starterPlatformStep;
        targetStep = starterPlatformStep;
        for (int i = 0; i < starterPlatformCount; i++)
        {
            GenerateNextPlatform(GlobalTileKey.NORMAL_TILE);
        }
    }
    private void GenerateNextPlatform(string _tileName = null)
    {
        if (platformsList.Count > maxPlatformCount)
            DisablePlatformAt(0,true);
        
        if (currentStep == targetStep)
        {
            if (retainCount > 0)
            {
                nextStep = currentStep;
                retainCount--;
            }
            else
            {
                targetStep = Mathf.Clamp(currentStep + Random.Range(-maxRandomStep, maxRandomStep + 1), -maxStep, maxStep);
                retainCount = Random.Range(1, maxRetainCount);
            }
        }
        else
        {
            nextStep += (currentStep < targetStep) ? 1 : -1;
        }
        
        AddNewPlatform(_tileName ?? RandomTile(commonTilePool), nextStep);
        currentStep = nextStep;
        currentPosition++;
    }
    private void AddNewPlatform(string _platformName, int _step)
    {
        var _obj = ObjectPooling.Instance.GetFromPool
            (_platformName, new Vector2(currentPosition, CalculateByStep((_step))), Quaternion.identity);
        if (_obj.TryGetComponent<TileModule>(out var _tileModule))
        {
            _tileModule.ResetModule();
        }
        platformsList.Add(_obj);
    }
    private string RandomTile(List<String> _pool)
    {
        return _pool[Random.Range(0, _pool.Count)];
    }
    public void DisablePlatformAt(int _index, bool _isRemove = false)
    {
        var _obj = platformsList[_index];
        _obj.SetActive(false);

        if (_isRemove)
            platformsList.RemoveAt(_index);
    }
    private float CalculateByStep(int _step)
    {
        return platformStartHeight + (_step * platformDifferenceHeight);
    }

    private void SpawnEnemy(string _enemyKey, int step)
    {
        
    }
    private void SetUpAllPool()
    {
        SetUpChancePool(commonTilePool, commonTileChance);
    }
    private void SetUpChancePool(List<String> _pool,List<TileSpawnChance> _chances)
    {
        _pool.Clear();
        foreach(var _c in _chances)
        {
            for(int i = 0; i < _c.SpawnChance; i++)
            {
                _pool.Add(_c.TileName);
            }
        }
    }
}

[System.Serializable]
public struct TileSpawnChance
{
    public String TileName;
    public float SpawnChance;
}