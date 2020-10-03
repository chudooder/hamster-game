using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float speed = 20f; // degrees per second

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: modify speed based on player input and natural deceleration
        this.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
