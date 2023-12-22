using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage;
    public float attackRate;
    public float attackRange;
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
        Debug.Log("Hitting");
        RaycastHit hit;
        if (
            Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hit,
                attackRange
            )
        )
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hitting Enemy");
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    void PlayAnimation()
    {
        //Anim stuff
    }
}
