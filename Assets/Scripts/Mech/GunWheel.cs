using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunWheel : MonoBehaviour, IPointerClickHandler {
    public Transform gunPivot;
    public float ACCEL = 60f;
    public float DECEL = 30f;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gunPivot.Rotate(new Vector3(0, 0, speed * Time.deltaTime));

        if (speed > 0) {
            speed = Mathf.Max(0, speed - DECEL * Time.deltaTime);
        } else {
            speed = Mathf.Min(0, speed + DECEL * Time.deltaTime);
        }
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
