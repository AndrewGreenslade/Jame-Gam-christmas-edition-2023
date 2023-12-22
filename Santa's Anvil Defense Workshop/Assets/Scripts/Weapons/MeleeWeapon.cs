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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && nextAttack <= Time.time)
        {
            PlayAnimation();
            CheckHit();
            nextAttack = Time.time + attackRate;
        }
    }

    void CheckHit()
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
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void PlayAnimation()
    {
        //Anim stuff
    }
}
