using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite filledHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Transform container;
    [SerializeField] private String heartKey = "Heart_UI";
    
    private List<GameObject> hearts = new List<GameObject>();
    
    public void InitLife(int _maxLife)
    {
        foreach (var _heart in hearts)
        {
            ObjectPooling.Instance.ReturnToPool(_heart);
        }
        hearts.Clear();
        
        for (int i = 0; i < _maxLife; i++)
        {
            GameObject _heartObj = ObjectPooling.Instance.GetFromPool(heartKey, Vector3.one, quaternion.identity);
            _heartObj.GetComponent<Image>().sprite = filledHeart;
            _heartObj.transform.SetParent(container);
            _heartObj.transform.localScale = Vector3.one;
            hearts.Add(_heartObj);
        }
    }
    
    public void UpdateLifeUI(int _currentLife)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Image _sprite = hearts[i].GetComponent<Image>();
            _sprite.sprite = i < _currentLife ? filledHeart : emptyHeart;
        }
    }
}
