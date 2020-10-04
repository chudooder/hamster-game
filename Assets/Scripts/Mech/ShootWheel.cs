using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootWheel : HamsterWheel, IPointerClickHandler {
    public float ACCEL = 60f;
    public float DECEL = 30f;
    public float MAX_SPEED = 180f;
    public float bulletThreshold = 60f;
    public Gun gun;

    private float speed;
    private float bulletCounter;

    private HammyRun hammyRun;
    private SpinToWin spin;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        hammyRun = GetComponentInChildren<HammyRun>();
        spin = GetComponentInChildren<SpinToWin>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Max(0, speed - DECEL * Time.deltaTime);
        bulletCounter += speed * Time.deltaTime;
        if (bulletCounter > bulletThreshold) {
            bulletCounter -= bulletThreshold;
            Shoot();
        }

        spin.Speed = -speed;
        hammyRun.SetSpeed(-0.05f * speed);
    }

    private void Shoot() {
        gun.Shoot();
    }

    public void OnPointerClick(PointerEventData eventData) {
        speed = Mathf.Min(speed + ACCEL, MAX_SPEED);
    }
}
