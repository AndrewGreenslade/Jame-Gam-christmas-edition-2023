using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage;
    public float attackRate;
    public float attackRange;
    public LayerMask mask;
    float nextAttack;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && nextAttack <= Time.time && !GameManager.Instance.anvilUI.isOpen)
        {
            anim.SetTrigger("Attack");
            nextAttack = Time.time + attackRate;
        }
    }

    public void CheckHit()
    {
        RaycastHit hit;
        if (
            Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                attackRange,
                mask
            )
        )
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public void PlayHit()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
}
