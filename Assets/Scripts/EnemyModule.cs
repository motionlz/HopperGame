using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class EnemyModule : MonoBehaviour, IDamageable, ISpawnObject
{
    [SerializeField] private AttackModule attackModule;
    [Header(("Enemy Stat"))]
    [SerializeField] private int healthPoint = 1;
    [SerializeField] private float preAttackTime;
    [SerializeField] private float cooldownTime;
    
    private int currentHealth;
    private bool isAttacking = false;
    public event Action OnDead;
    
    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Dead();
            OnDead?.Invoke();
        }
    }

    public void ResetModule()
    {
        currentHealth = healthPoint;
        isAttacking = false;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag(GlobalTag.PLAYER_TAG) && !isAttacking)
        {
            //StartAttack();
            StartCoroutine(AttackCoroutine());
        }
    }

    private async void StartAttack()
    {
        isAttacking = true;
        await Task.Delay((int)(preAttackTime * 1000));
        while (isAttacking)
        {
           attackModule.Attack(); 
           await Task.Delay((int)(cooldownTime * 1000)); 
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(preAttackTime);
        while (isAttacking)
        {
           attackModule.Attack(); 
           yield return new WaitForSeconds(cooldownTime);
        }
    }
}
