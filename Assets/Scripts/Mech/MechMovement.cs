using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float MAX_SPEED = 30f;
    [HideInInspector] public float speed = 20f; // degrees per second
    public float scoreModifier = 10000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        GameManager.instance.AddScore((int)(this.speed * Time.deltaTime * scoreModifier));
    }

    public void IncreaseSpeed(float speed) {
        this.speed = Mathf.Clamp(this.speed + speed, 0, MAX_SPEED);
    }
}
