using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableObject : MonoBehaviour, ISpawnObject, IInteractable
{
    public virtual void ResetModule()
    {
        gameObject.SetActive(true);
    }
    public virtual void OnInteract()
    {
        gameObject.SetActive(false);
    }

    public virtual void PlayModule() { }
}
