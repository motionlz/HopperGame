using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModule : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private bool isAttackLeft = true;
    [SerializeField] private Vector2 attackSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask targetLayer;

    public void Attack()
    {
        RaycastHit2D[] _hit = Physics2D.BoxCastAll(
            transform.position, attackSize, 0, isAttackLeft? Vector2.left : Vector2.right, attackRange, targetLayer
        );
        foreach (var _enemy in _hit)
        {
            if (_enemy.collider.TryGetComponent<IDamageable>(out var _target))
            {
                _target.TakeDamage(attackDamage);
            }
        }
    }
}
