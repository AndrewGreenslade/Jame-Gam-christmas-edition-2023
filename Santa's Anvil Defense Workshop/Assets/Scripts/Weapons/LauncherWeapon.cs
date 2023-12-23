using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherWeapon : Weapon
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileForce;

    public override void Shoot()
    {
        currentAmmo--;
        GameObject temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.GetComponent<Rigidbody>().AddForce(temp.transform.forward * projectileForce);
    }
}
