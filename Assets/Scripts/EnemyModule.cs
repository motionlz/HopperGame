using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class EnemyModule : UnitModule, ISpawnObject
{
    [SerializeField] private AttackModule attackModule;
    [Header(("Enemy Stat"))]
    [SerializeField] protected float preAttackTime;
    [SerializeField] protected float cooldownTime;
    
    private bool isAttacking = false;

    public virtual void ResetModule()
    {
        ResetUnit();
        isAttacking = false;
    }

    protected override void Dead()
    {
        gameObject.SetActive(false);
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag(GlobalTag.PLAYER_TAG) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(preAttackTime);
        while (isAttacking)
        {
           attackModule.Attack(); 
           yield return new WaitForSeconds(cooldownTime);
        }
    }
    
    public void PlayModule() { }
}
