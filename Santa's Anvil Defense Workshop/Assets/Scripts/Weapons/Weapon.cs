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

    private void Start()
    {
        currentAmmo = magSize;
        anim.keepAnimatorStateOnDisable = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire") && !GameManager.Instance.anvilUI.isOpen)
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
        if (Input.GetKeyUp(KeyCode.Mouse0) && !GameManager.Instance.anvilUI.isOpen)
        {
            anim.SetBool("Shooting", false);
        }
        if (Input.GetKeyDown(KeyCode.R) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") && !GameManager.Instance.anvilUI.isOpen)
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
        anim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magSize;
    }

    private void OnEnable()
    {
        audioSource.Stop();
        anim.Rebind();
        anim.Update(0f);
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
