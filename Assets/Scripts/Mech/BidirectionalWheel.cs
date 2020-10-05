using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BidirectionalWheel : HamsterWheel, IPointerClickHandler {
    public Transform pivot;
    // public float ACCEL_SCALE = 60f;
    // public float DECEL_SCALE = 30f;
    // public float MAX_SPEED_SCALE = 50f;
    
    public float BASE_ACCEL = 60f;
    public float BASE_DECEL = 30f;
    public float BASE_MAX_SPEED = 50f;

    public float ACCEL => Hamster == null ? 0 : Hamster.GetStat(Stats.StatType.Acceleration) / 5 * BASE_ACCEL + BASE_ACCEL;
    public float DECEL => Hamster == null ? BASE_DECEL : (5 - Hamster.GetStat(Stats.StatType.Motivation)) / 5 * BASE_DECEL + BASE_DECEL;
    public float MAX_SPEED => Hamster == null ? BASE_MAX_SPEED : Hamster.GetStat(Stats.StatType.Speed) / 5 * BASE_MAX_SPEED + BASE_MAX_SPEED;

    
    private float speed;
    private HammyRun hammyRun;
    private SpinToWin spin;

    // Start is called before the first frame update
    void Start()
    {
        hammyRun = GetComponentInChildren<HammyRun>();
        spin = GetComponentInChildren<SpinToWin>();
    }

    // Update is called once per frame
    void Update()
    {
        pivot.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

        if (speed > 0) {
            speed = Mathf.Clamp(speed - DECEL * Time.deltaTime, 0, MAX_SPEED);
        } else {
            speed = Mathf.Clamp(speed + DECEL * Time.deltaTime, -MAX_SPEED, 0);
        }

        spin.Speed = 5 * speed;
        hammyRun.SetSpeed(0.25f * speed);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 localPos = transform.InverseTransformPoint(worldPos);
        if (localPos.x < 0) {
            speed += ACCEL;
        } else {
            speed -= ACCEL;
        }

        HamsterCard.CurrentHamster = Hamster;
    }
}
