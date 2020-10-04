using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public float lifespan = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Expire());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    private IEnumerator Expire() {
        yield return new WaitForSeconds(lifespan);
        Destroy(this.gameObject);
    }
}
