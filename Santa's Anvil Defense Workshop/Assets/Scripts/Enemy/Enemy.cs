using System.Collections;
using ECM.Controllers;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform currentTarget;

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
        StartCoroutine(CheckClosest());
    }
}
