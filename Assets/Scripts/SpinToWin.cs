using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinToWin : MonoBehaviour
{
    public float Speed = 3;
    private MechMovement mechMovement;

    private void Awake() {
        this.mechMovement = GetComponentInParent<MechMovement>();
    }

    private void Update() {
        this.Speed = -mechMovement.speed * 20;
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
