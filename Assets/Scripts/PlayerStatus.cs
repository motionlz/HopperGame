using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerModule, IDamageable
{
    private int healthPoint = 3;
    
    public void TakeDamage(int _damage)
    {
        if (PlayerManager.PlayerActionModule.IsBlock())
        {
            Debug.Log("Blocked");
            return;
        }
        
        healthPoint -= _damage;
        Debug.Log("Taking Damage");
        
        if (healthPoint <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
