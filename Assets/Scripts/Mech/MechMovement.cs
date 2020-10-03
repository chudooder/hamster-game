using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float MAX_SPEED = 30f;
    public float speed = 20f; // degrees per second
    public float deceleration = 2f; // degrees per second squared
    public float speedPerClick = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.speed = Mathf.Clamp(this.speed - deceleration * Time.deltaTime, 0, MAX_SPEED);
        this.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void IncreaseSpeed() {
        this.speed += speedPerClick;
    }
}
