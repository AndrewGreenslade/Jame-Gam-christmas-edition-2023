using System.Collections;
using ECM.Controllers;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform currentTarget;
    public GameObject model;
    public GameObject ragdoll;
    public Animator anim;

    public int damage;
    public float attackRate;
    float nextAttack;

    int health = 100;

    private void Awake()
    {
        currentTarget = GameManager.Instance.Anvil.transform;
        StartCoroutine(CheckClosest());
    }

    private void Update()
    {
        agent.destination = currentTarget.position;
    }

    IEnumerator CheckClosest()
    {
        yield return new WaitForSeconds(2);
        if (
            Vector3.Distance(
                transform.position,
                BaseFirstPersonController.Instance.transform.position
            ) < Vector3.Distance(transform.position, GameManager.Instance.Anvil.transform.position)
        )
        {
            currentTarget = BaseFirstPersonController.Instance.transform;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetBool("Stopped", true);
        }
        else
        {
            anim.SetBool("Stopped", false);
        }

        StartCoroutine(CheckClosest());
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
        {
            return;
        }
        anim.SetTrigger("DamageTaken");
        health -= damage;
        if (health <= 0)
        {
            model.SetActive(false);
            ragdoll.SetActive(true);
            //Do death animation?
            GameManager.Instance.kills++;
            Destroy(gameObject, 5);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (nextAttack < Time.time)
        {
            if (other.tag == "Anvil")
            {
                GameManager.Instance.Anvil.DealDamage(1);
                anim.SetTrigger("Attack");
                nextAttack = Time.time + attackRate;
            }
            else if (other.tag == "Player")
            {
                other.GetComponent<Player>().DealDamage(damage);
                anim.SetTrigger("Attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }
}
