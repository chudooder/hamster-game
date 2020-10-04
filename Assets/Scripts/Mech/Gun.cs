using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletPrefab;
    public Transform bulletSpawnPoint;

    // Update is called once per frame
    public void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab);
        bullet.position = bulletSpawnPoint.position;
        bullet.rotation = bulletSpawnPoint.rotation;
    }
}
