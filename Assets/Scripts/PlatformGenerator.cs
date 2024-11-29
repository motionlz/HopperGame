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
    [SerializeField]private List<SpawnChance> commonTileChance = new List<SpawnChance>();

    [Header("Enemy Settings")] 
    [SerializeField]private List<SpawnChance> enemyTypeChance = new List<SpawnChance>();
    
    
    private int retainCount;
    private int currentStep;
    private int nextStep;
    private int targetStep;
    private int currentPosition = 0;
    private List<GameObject> platformsList = new List<GameObject>();
    private List<String> commonTilePool = new List<String>();
    private List<String> enemyPool = new List<string>();

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
            GenerateNextPlatform(GlobalTileKey.NORMAL_TILE,false);
        }
    }
    private void GenerateNextPlatform(string _tileName = null,bool isSpawnable = true)
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

        _tileName = _tileName ?? RandomPool(commonTilePool);
        var _position = new Vector2(currentPosition, CalculateByStep((nextStep)));

        var _obj = SpawnObjectFromPool(_tileName, _position, platformsList);
        _obj.GetComponent<TileModule>().SetTileActive(true);
        
        if(_tileName == GlobalTileKey.NORMAL_TILE && isSpawnable)
            GenerateEnemy(_position);

        currentStep = nextStep;
        currentPosition++;
    }
    private void GenerateEnemy(Vector2 _tilePosition, String _enemyKey = null)
    {
        SpawnObjectFromPool
            (_enemyKey ?? RandomPool(enemyPool),_tilePosition + Vector2.up);
    }
    private string RandomPool(List<String> _pool)
    {
        return _pool[Random.Range(0, _pool.Count)];
    }

    public int GetTileIndex(GameObject _obj)
    {
        return platformsList.IndexOf(_obj);
    }
    public void DisablePlatformAt(int _index, bool _isRemove = false)
    {
        if (_index < 0 || _index >= platformsList.Count) return;
        
        var _obj = platformsList[_index];
        _obj.GetComponent<TileModule>().SetTileActive(false);

        if (!_isRemove) return;
        platformsList.RemoveAt(_index);
        ObjectPooling.Instance.ReturnToPool(_obj);
    }
    private float CalculateByStep(int _step)
    {
        return platformStartHeight + (_step * platformDifferenceHeight);
    }
    
    private GameObject SpawnObjectFromPool(string _key, Vector2 _position, List<GameObject> _listToAdd = null)
    {
        if (_key == GlobalTileKey.EMPTY) return null;
        var _obj = ObjectPooling.Instance.GetFromPool
            (_key, _position, Quaternion.identity);

        _listToAdd?.Add(_obj);

        return _obj;
    }

    private void SetUpAllPool()
    {
        SetUpChancePool(commonTilePool, commonTileChance);
        SetUpChancePool(enemyPool, enemyTypeChance);
    }
    private void SetUpChancePool(List<String> _pool,List<SpawnChance> _chances)
    {
        _pool.Clear();
        foreach(var _c in _chances)
        {
            for(int i = 0; i < _c.Chance; i++)
            {
                _pool.Add(_c.KeyName);
            }
        }
    }
}

[System.Serializable]
public struct SpawnChance
{
    public String KeyName;
    public float Chance;
}