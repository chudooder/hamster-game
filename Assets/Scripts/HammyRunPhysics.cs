using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammyRunPhysics : HammyRun
{
    public float SLIP_DOWN_FACTOR = 5;
    public float speed = 0;
    public float angle;
    public float angularVel; // relative to wheel
    public float angularAccel;
    public float slipDownVel;
    public float wheelAngularVel;
    private float lastSpeed;
    public float direction;
    private bool speedUpRequested;

    // Start is called before the first frame update
    void Start()
    {
        this.angle = 0f;
        this.lastSpeed = speed;
    }

    void FixedUpdate()
    {
        // direction hamster is running. opposite of motion of wheel/apparatus
        this.direction = -Mathf.Sign(speed);
        if (speedUpRequested) {
            this.angularAccel = 50000 * direction;
        } else {
            this.angularAccel = -400 * direction;
        }
        wheelAngularVel = 20 * speed;
        // Slip down the sides of the wheel
        slipDownVel = this.angle * -SLIP_DOWN_FACTOR;
        if (direction > 0) {
            this.angularVel = Mathf.Clamp(this.angularVel + this.angularAccel * Time.fixedDeltaTime, wheelAngularVel * 1.2f, 0);
        } else if (direction < 0) {
            this.angularVel = Mathf.Clamp(this.angularVel + this.angularAccel * Time.fixedDeltaTime, 0, wheelAngularVel * 1.2f);
        }

        float change = (this.angularVel + wheelAngularVel + slipDownVel) * Time.fixedDeltaTime;

        this.angle += change;
        if (this.angle < -180f) {
            this.angle += 360;
        } else if (this.angle > 180f) {
            this.angle -= 360;
        }

        speedUpRequested = false;
        lastSpeed = speed;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public override void SetSpeed(float speed) {
        speed = -speed;
        if (Mathf.Abs(this.speed) < Mathf.Abs(speed)) {
            speedUpRequested = true;
        }
        this.speed = speed;
    }
}
