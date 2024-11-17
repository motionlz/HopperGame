using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TileModule : MonoBehaviour, ISpawnObject
{
    [SerializeField] private bool enableTrigger;
    [SerializeField] private float delayTime;
    public event Action OnReset;
    public event Action OnTrigger;
    
    public virtual void ResetModule()
    {
        OnReset?.Invoke();
    }

    protected virtual async void TriggerModuleAsync()
    {
        //Debug.Log($"Trigger Module in {+(int)(delayTime * 1000)} ms");
        await Task.Delay((int)(delayTime * 1000));
        TriggerModule();
        OnTrigger?.Invoke();
    }

    protected virtual void TriggerModule() { }

    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (enableTrigger && _col.CompareTag(GlobalTag.PLAYER_TAG))
        {
            TriggerModuleAsync();
        }
    }
}
