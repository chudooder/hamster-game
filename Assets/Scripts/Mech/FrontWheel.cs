using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWheel : MonoBehaviour
{
    private HammyRun hammyRun;
    private MechMovement mechMovement;

    private void Awake() {
        this.hammyRun = GetComponentInChildren<HammyRun>();
        this.mechMovement = GetComponentInParent<MechMovement>();
    }

    private void OnMouseDown() {
        this.mechMovement.IncreaseSpeed();
    }

    private void Update() {
        hammyRun.SetSpeed(mechMovement.speed);
    }
}
