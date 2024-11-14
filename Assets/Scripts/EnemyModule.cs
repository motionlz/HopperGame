using System.Threading;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyModule : MonoBehaviour
{
    [Header(("Enemy Stat"))]
    [SerializeField] private int healthPoint = 1;
    [SerializeField] private int atkDamage = 1;
    [SerializeField] private Transform atkPosition;
    [SerializeField] private Vector2 atkSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private float preAttackTime;
    [SerializeField] private float cooldownTime;
    
    private int currentHealth;
    private bool isAttacking = false;
    
    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            OnDead();
        }
    }

    public void ResetModule()
    {
        currentHealth = healthPoint;
        isAttacking = false;
    }

    private void OnDead()
    {
        gameObject.SetActive(false);
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag(GlobalTag.PLAYER_TAG) && !isAttacking)
        {
            StartAttack();
        }
    }

    private async void StartAttack()
    {
        isAttacking = true;
        await Task.Delay((int)(preAttackTime * 1000));
        while (isAttacking)
        {
           Attack(); 
           await Task.Delay((int)(cooldownTime * 1000)); 
        }
    }

    private void Attack()
    {
        if (!isAttacking) return;
        RaycastHit2D[] _hit = Physics2D.BoxCastAll(
            transform.position, atkSize, 0, Vector2.left
        );
        foreach (var _p in _hit)
        {
            if (_p.collider.TryGetComponent<PlayerStatus>(out PlayerStatus _player))
            {
                _player.TakeDamage(atkDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (atkPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(atkPosition.position, atkSize);
    }
}
