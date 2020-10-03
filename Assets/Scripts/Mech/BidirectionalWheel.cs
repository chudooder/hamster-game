using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BidirectionalWheel : MonoBehaviour, IPointerClickHandler {
    public Transform pivot;
    public float ACCEL = 60f;
    public float DECEL = 30f;
    public float MAX_SPEED = 50f;
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
    }
}
