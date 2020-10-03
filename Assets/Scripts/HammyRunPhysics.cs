using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammyRunPhysics : MonoBehaviour
{
    private MechMovement mechMovement;
    public float SLIP_DOWN_FACTOR = 5;
    private float angle;
    private float angularVel; // relative to wheel
    private float angularAccel;
    private float lastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.mechMovement = GetComponentInParent<MechMovement>();
        this.angle = 0f;
        this.lastSpeed = mechMovement.speed;
    }

    void FixedUpdate()
    {
        float currentSpeed = mechMovement.speed;
        if (currentSpeed > lastSpeed) {
            this.angularAccel = 50000;
        } else {
            this.angularAccel = -400;
        }
        float wheelAngularVel = -20 * mechMovement.speed;
        // Slip down the sides of the wheel
        float slipDownVel = this.angle * -SLIP_DOWN_FACTOR;
        this.angularVel = Mathf.Clamp(this.angularVel + this.angularAccel * Time.fixedDeltaTime, 0, -wheelAngularVel * 1.2f);
        this.angle += (this.angularVel + wheelAngularVel + slipDownVel) * Time.fixedDeltaTime;
        if (this.angle < -180f) {
            this.angle += 360;
        } else if (this.angle > 180f) {
            this.angle -= 360;
        }

        lastSpeed = currentSpeed;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
