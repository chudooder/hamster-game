using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public float lifespan = 1f;
    public int damage = 2;
    
    [SerializeField] private GameObject _deathEffect;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
            enemy.Damage(2);
        else 
        
        if (_deathEffect) Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
