using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitModule : MonoBehaviour, IDamageable
{
    [SerializeField] protected int healthPoint = 3;
    [SerializeField] protected float invincibleTime = 0.1f;

    private int currentHealth;
    private bool isInvincible;
    private Coroutine invincibleCoroutine;
    
    public event Action<int> OnHealthChanged;
    public event Action OnDeath;
    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible) return;
        currentHealth -= _damage;
        UpdateLifeUI(currentHealth);

        SetInvincible();
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    protected void SetInvincible()
    {
        invincibleCoroutine ??= StartCoroutine(Invincible());
    }
    private IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        
        isInvincible = false;
        StopInvincible();
    }
    
    protected virtual void Dead()
    {
        OnDeath?.Invoke();
    }
    public void ForceDead()
    {
        currentHealth = 0;
        UpdateLifeUI(currentHealth);
        Dead();
    }
    protected void ResetUnit()
    {
        SetFullHealth();
        StopInvincible();
    }

    public void SetFullHealth()
    {
        currentHealth = healthPoint;
        UpdateLifeUI(currentHealth);
    }
    private void StopInvincible()
    {
        if (invincibleCoroutine == null) return;
        
        StopCoroutine(invincibleCoroutine);
        invincibleCoroutine = null;
        isInvincible = false;
    }

    private void UpdateLifeUI(int _life)
    {
        OnHealthChanged?.Invoke(_life);
    }
    public int GetMaxHealth()
    {
        return healthPoint;
    }

    public bool IsHealthFull()
    {
        return currentHealth == healthPoint;
    }
}
