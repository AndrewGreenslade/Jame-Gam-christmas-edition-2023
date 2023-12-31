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
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public int damage;
    public float attackRate;
    float nextAttack;

    int health = 100;
    public bool inGame = true;

    private void Awake()
    {
        if (!inGame) { StartCoroutine(RandomTarget()); return; }
        currentTarget = GameManager.Instance.Anvil.transform;
        StartCoroutine(CheckClosest());
    }

    private void Start()
    {

        Rigidbody[] rbs = ragdoll.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        agent.destination = currentTarget.position;
    }

    IEnumerator RandomTarget()
    {

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // anim.SetBool("Stopped", true);
            var points = GameObject.FindGameObjectsWithTag("MainMenuPoint");
            currentTarget = points[Random.Range(0, points.Length)].transform;
        }
        else
        {
            // anim.SetBool("Stopped", false);
        }
        yield return new WaitForSeconds(1);

        StartCoroutine(RandomTarget());
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
        PlayTakeDamage();
        if (health <= 0)
        {
            return;
        }
        anim.SetTrigger("DamageTaken");
        health -= damage;
        if (health <= 0)
        {
            PlayDead();
            // model.SetActive(false);
            // ragdoll.SetActive(true);
            //Do death animation?
            Dead();
            GameManager.Instance.kills++;
            gameObject.layer = 2;
            Destroy(gameObject, 5);
        }
    }

    public void Dead()
    {
        Rigidbody[] rbs = ragdoll.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rbs)
        {
            rb.isKinematic = false;
            anim.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!inGame) { return; }
        if (nextAttack < Time.time && health > 0)
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

    public void PlayTakeDamage()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    public void PlayAttack()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
    public void PlayDead()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
