using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOneShot = false;
    [SerializeField] private int damage;

    public virtual void OnInteract()
    {
        if (isOneShot) 
            GameManager.Instance.PlayerManager.ForceDead();
        else
            GameManager.Instance.PlayerManager.TakeDamage(damage);
    }
}
