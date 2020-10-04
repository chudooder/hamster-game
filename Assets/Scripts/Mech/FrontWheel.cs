using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrontWheel : HamsterWheel, IPointerClickHandler
{
    private HammyRun hammyRun;
    private SpinToWin spin;
    private MechMovement mechMovement;

    private void Awake() {
        this.spin = GetComponentInChildren<SpinToWin>();
        this.hammyRun = GetComponentInChildren<HammyRun>();
        this.mechMovement = GetComponentInParent<MechMovement>();
    }

    private void Update() {
        spin.Speed = -20f * mechMovement.speed;
        hammyRun.SetSpeed(-0.5f * mechMovement.speed);
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.mechMovement.IncreaseSpeed();
    }
}
