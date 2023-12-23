using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;
    public int magSize;
    public float reloadTime;
    public float maxDistance;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    float nextFire;
    public int currentAmmo;
    bool reloading;

    private void Start()
    {
        currentAmmo = magSize;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !reloading && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            if (nextFire < Time.time)
            {
                if (currentAmmo > 0)
                {
                    anim.SetBool("Shooting", true);
                }
                else
                {
                    PlayEmpty();
                    anim.SetBool("Shooting", false);
                    StartCoroutine(Reload());
                }
                nextFire = Time.time + fireRate;
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

    public string GetAmmo()
    {
        return currentAmmo.ToString();
    }

    public void PlayShoot()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    public void PlayReload()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void PlayEmpty()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
