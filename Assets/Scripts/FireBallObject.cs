using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallObject : DamageObject
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Space trackingSpace = Space.Self;

    private void Update()
    {
        MoveLeft();
    }
    private void MoveLeft()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime), trackingSpace);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        this.gameObject.SetActive(false);
    }
}
