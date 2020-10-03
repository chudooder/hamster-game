using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWin : MonoBehaviour
{
    public float Speed = 3;

    private void Update() {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
