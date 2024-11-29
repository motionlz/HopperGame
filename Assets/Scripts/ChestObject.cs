using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : MonoBehaviour, IDamageable
{
    [SerializeField] private Sprite closeChest;
    [SerializeField] private Sprite openChest;
    [SerializeField] private SpriteRenderer chestSprite;
    
    [Header("Drop Settings")]
    [SerializeField] private int potionChance = 33;
    [SerializeField] private int coinMinDrop = 3;
    [SerializeField] private int coinMaxDrop = 6;
    
    private bool isOpen = false;
    public void TakeDamage(int _damage)
    {
        OpenChest();
    }

    public void ResetObject()
    {
        isOpen = false;
        ChestSpriteOpen(false);
    }

    private void OpenChest()
    {
        if (isOpen) return;
        
        isOpen = true;
        ChestSpriteOpen(true);

        if (RandomUtility.RandomPercentagePass(potionChance) 
            && !GameManager.Instance.PlayerManager.IsHealthFull())
        {
            DropPotion();
        }
        else
        {
            DropCoin();
        }
    }

    private void DropPotion()
    {
        GetAndPopObject(DropItemKey.POTION_OBJECT);
    }

    private void DropCoin()
    { 
        var _count = Random.Range(coinMinDrop, coinMaxDrop + 1);
        for (int i = 0; i < _count; i++)
        {
            GetAndPopObject(DropItemKey.COIN_OBJECT);
        }
    }

    private void GetAndPopObject(string _key)
    {
        var _obj = ObjectPooling.Instance.GetFromPool
            (_key, transform.position, Quaternion.identity);
        _obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0f, 1f),Random.Range(2f, 5f));
    }
    private void ChestSpriteOpen(bool _isOpen)
    {
        chestSprite.sprite = _isOpen ? openChest : closeChest;
    }
}

public static class DropItemKey
{
    public const string POTION_OBJECT = "Potion_Object";
    public const string COIN_OBJECT = "Coin_Object";
}