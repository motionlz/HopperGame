using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTile : TileModule
{
    protected override void TriggerModule()
    {
        base.TriggerModule();
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D _rb))
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public override void ResetModule()
    {
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D _rb))
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        base.ResetModule();
    }
}
