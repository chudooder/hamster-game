using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FrontWheel : HamsterWheel, IPointerClickHandler
{
    public float BASE_ACCEL = 2f;
    public float BASE_DECEL = 2f;

    public float ACCEL => Hamster == null ? BASE_ACCEL : Hamster.GetStat(Stats.StatType.Acceleration) / 5 * BASE_ACCEL + BASE_ACCEL;
    public float DECEL => Hamster == null ? BASE_DECEL : (5 - Hamster.GetStat(Stats.StatType.Motivation)) / 5 * BASE_DECEL + BASE_DECEL;

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
        this.mechMovement.IncreaseSpeed(-DECEL * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.mechMovement.IncreaseSpeed(ACCEL);
    }
}
