using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minY = 1f;
    [SerializeField] private float maxY = 3.5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform attachTransform;
    
    private int direction = 1;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float _yPos = transform.localPosition.y + direction * speed * Time.deltaTime;
        
        if (_yPos >= maxY)
        {
            direction = -1;
        }
        else if (_yPos <= minY)
        {
            direction = 1;
        }
        
        _yPos = Mathf.Clamp(_yPos, minY, maxY);
        
        transform.localPosition = new Vector3(transform.localPosition.x, _yPos, transform.localPosition.z);
    }
}
