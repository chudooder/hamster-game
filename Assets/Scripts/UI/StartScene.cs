using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private bool hasStarted = false;
    
    public void StartGame()
    {
        GameManager.instance.BeginRun();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !hasStarted)
        {
            StartGame();
            hasStarted = true;
        }
    }
}
