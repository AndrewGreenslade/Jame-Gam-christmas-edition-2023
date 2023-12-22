using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public int magSize;
    public float reloadTime;
    public float maxDistance;
    public Animator anim;
    float nextFire;
    [HideInInspector]
    public int currentAmmo;
    bool reloading;

    private void Start()
    {
        currentAmmo = magSize;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !reloading)
        {
            if (nextFire < Time.time)
            {
                if (currentAmmo > 0)
                {
                    anim.SetBool("Shooting", true);
                    Shoot();
                }
                else
                {
                    StartCoroutine(Reload());
                }
                nextFire = Time.time + fireRate;
            }
            else
            {
                anim.SetBool("Shooting", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("Shooting", false);
        }
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(Reload());
        }
    }

    public virtual void Shoot()
    {
        Debug.Log("Did not implement shooting for this weapon.");
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        reloading = true;
        anim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magSize;
        reloading = false;
    }
}
