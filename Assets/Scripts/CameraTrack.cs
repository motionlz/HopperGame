using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float offSetX = 2.85f;
    private void Update()
    {
        transform.position = new Vector3(playerPosition.position.x - offSetX, transform.position.y, transform.position.z);
    }
}
