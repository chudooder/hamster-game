using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechAudio : MonoBehaviour
{
    private MechMovement mechMovement;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.mechMovement = GetComponent<MechMovement>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Mathf.Lerp(0.2f, 0.8f, mechMovement.speed / mechMovement.MAX_SPEED);
    }
}
