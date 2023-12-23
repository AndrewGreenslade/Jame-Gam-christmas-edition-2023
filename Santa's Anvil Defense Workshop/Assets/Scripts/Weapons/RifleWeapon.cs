using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    public LayerMask mask;
    public override void Shoot()
    {
        RaycastHit hit;
        currentAmmo--;
        if (
            Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit, maxDistance, mask
            )
        )
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
