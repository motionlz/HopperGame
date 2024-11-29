using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [Header("Track Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float offset = 10f;
    [Header("Fireball Settings")]
    [SerializeField] private GameObject warningIndicator;
    [SerializeField] private GameObject fireBallObject;
    [SerializeField] private float warnTime = 2f;
    [SerializeField] private float delayTime = 5f;
    [SerializeField] private float fireChance = 5f;
    
    private bool isFiring = false;

    public void Init()
    {
        GameManager.Instance.PlayerManager.PlayerActionModule.OnJump += StartFire;
    }
    private void LateUpdate()
    {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        transform.position = GetSpawnPosition();
    }

    private Vector2 GetSpawnPosition()
    {
        return new Vector2(playerTransform.position.x + offset, playerTransform.position.y);
    }
    private void StartFire()
    {
        if (isFiring || !RandomUtility.RandomPercentagePass(fireChance)) return;
        
        StartCoroutine(Fire());
    }
    private void WarningFire(bool _isActive)
    {
        warningIndicator.SetActive(_isActive);
        warningIndicator.transform.position = playerTransform.position;
    }

    private IEnumerator Fire()
    {
        isFiring = true;
        WarningFire(true);
        yield return new WaitForSeconds(warnTime);
        
        WarningFire(false);
        LaunchFireBall();
        yield return new WaitForSeconds(delayTime);
        isFiring = false;
    }

    private void LaunchFireBall()
    {
        fireBallObject.transform.position = GetSpawnPosition();
        fireBallObject.SetActive(true);
    }
}
