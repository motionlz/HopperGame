using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaManager : MonoBehaviour
{
    [Header("Lava Settings")]
    [SerializeField] private float minLavaHeight = -5f;
    [SerializeField] private float maxLavaHeight = 3f;
    [SerializeField] private float lavaSpeed = 0.2f;
    [SerializeField] private float lavaDecreasePerJump = 0.5f;
    [SerializeField] Transform playerPosition;
    
    private float currentLavaHeight = -5f;
    
    public void Init()
    {
        currentLavaHeight = minLavaHeight;
        GameManager.Instance.PlayerManager.PlayerActionModule.OnJump += LavaDecrease;
    }

    private void Update()
    {
        LaveRise();
    }

    private void LaveRise()
    {
        currentLavaHeight = HeightChange(Time.deltaTime * lavaSpeed);
        LavaUpdate();
    }

    private void LavaDecrease()
    {
        currentLavaHeight = HeightChange(-lavaDecreasePerJump);
        LavaUpdate();
    }

    private void LavaUpdate()
    {
        transform.position = new Vector3(playerPosition.position.x, currentLavaHeight, transform.position.z);
    }

    private float HeightChange(float _value)
    {
        return Mathf.Clamp(currentLavaHeight + _value, minLavaHeight, maxLavaHeight);
    }
}
