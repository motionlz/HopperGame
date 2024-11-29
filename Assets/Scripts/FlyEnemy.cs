using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyModule, IInteractable
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 2f;
    
    private FlyState flyState = FlyState.Stay;
    private Coroutine attackCoroutine;
    private void Update()
    {
        switch (flyState)
        {
            case FlyState.FlyBack:
                FlyToLocalPosition(new Vector3(2,1,10));
                break;
            case FlyState.FlyAttack:
                FlyToPosition(GameManager.Instance.PlayerManager.transform.position);
                break;
            default:
                break;
        }
    }

    public override void ResetModule()
    {
        base.ResetModule();
        flyState = FlyState.Stay;
        StopAttackCoroutine();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        if (!this.gameObject.activeInHierarchy) return;
        StartAttackCoroutine();
    }

    protected override IEnumerator AttackCoroutine()
    {
        FlyOff();
        yield return new WaitForSeconds(cooldownTime);
        
        FlyToAttack();
        StopAttackCoroutine();
    }
    private void StartAttackCoroutine()
    {
        attackCoroutine ??= StartCoroutine(AttackCoroutine());
    }
    private void StopAttackCoroutine()
    {
        if (attackCoroutine == null) return;
        
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }

    private void FlyOff()
    {
        SetInvincible();
        transform.SetParent(GameManager.Instance.MainCamera.transform);
        flyState = FlyState.FlyBack;
    }

    private void FlyToAttack()
    {
        flyState = FlyState.FlyAttack;
    }

    private void FlyToLocalPosition(Vector3 _pos)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition,
            _pos, speed * Time.deltaTime);
    }

    private void FlyToPosition(Vector3 _pos)
    {
        transform.position = Vector2.MoveTowards(transform.position,
            _pos, speed * Time.deltaTime);
    }
    
    public void OnInteract()
    {
        GameManager.Instance.PlayerManager.TakeDamage(damage);
        StartAttackCoroutine();
    }
    private enum FlyState
    {
        Stay,FlyBack,FlyAttack
    }
}


