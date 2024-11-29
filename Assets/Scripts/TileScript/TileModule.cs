using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TileModule : MonoBehaviour, ISpawnObject, IInteractable
{
    [SerializeField] protected float delayTime;
    [SerializeField] private bool isLoop;
    [SerializeField] protected GameObject baseTile;
    public event Action OnReset;
    public event Action OnTrigger;
    
    protected bool isOn = true;
    private Coroutine loopCoroutine;

    public virtual void PlayModule()
    {
        StartLoop();
    }
    public virtual void ResetModule()
    {
        OnReset?.Invoke();
        StopLoop();
    }

    public void SetTileActive(bool _isActive)
    {
        baseTile.SetActive(_isActive);
    }

    protected virtual async void TriggerModuleAsync()
    {
        //Debug.Log($"Trigger Module in {+(int)(delayTime * 1000)} ms");
        await Task.Delay((int)(delayTime * 1000));
        TriggerModule();
        OnTrigger?.Invoke();
    }

    protected virtual void TriggerModule() { }
    
    public void OnInteract()
    {
        TriggerModuleAsync();
    }
    
    protected virtual void StartLoop()
    {
        if (!isLoop) return;
        isOn = true;
        loopCoroutine ??= StartCoroutine(LoopBehavior());
    }
    protected virtual void StopLoop()
    {
        if (loopCoroutine == null) return;
        
        StopCoroutine(loopCoroutine);
        loopCoroutine = null;
        isOn = false;
    }

    protected virtual IEnumerator LoopBehavior()
    {
        yield return null;
    }
}
