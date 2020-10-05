using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidLocation : HamsterLocation
{
    public override Hamster Hamster
    {
        get
        {
            return null;
        }
        set
        {
            if (value != null)
            {
                HamsterManager.instance.currentHamsters.Remove(value);
                
                Destroy(value.gameObject);
            }
            _hamster = null;
        }
    }

    public void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        var b = sr.bounds;
        var corner = Camera.main.ViewportToWorldPoint(Vector2.zero);
        transform.position = (Vector2)(corner + b.max - b.min);
    }
}
