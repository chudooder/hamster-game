using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrontWheel : MonoBehaviour, IPointerClickHandler
{
    private HammyRun hammyRun;
    private MechMovement mechMovement;

    private void Awake() {
        this.hammyRun = GetComponentInChildren<HammyRun>();
        this.mechMovement = GetComponentInParent<MechMovement>();
    }

    private void Update() {
        hammyRun.SetSpeed(mechMovement.speed);
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.mechMovement.IncreaseSpeed();
    }
}
