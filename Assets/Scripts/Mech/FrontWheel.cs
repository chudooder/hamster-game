using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWheel : MonoBehaviour
{
    private MechMovement mechMovement;

    private void Awake() {
        this.mechMovement = GetComponentInParent<MechMovement>();
    }

    private void OnMouseDown() {
        this.mechMovement.IncreaseSpeed();
    }
}
