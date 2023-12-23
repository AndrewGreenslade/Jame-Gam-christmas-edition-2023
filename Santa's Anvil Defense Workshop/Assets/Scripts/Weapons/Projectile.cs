using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;
    public float radius;
    public float power;
    bool impacted;

    public AudioSource audioSource;
    public AudioClip explosionClip;
    public GameObject model;
    public GameObject explosion;
    private void OnCollisionEnter(Collision other)
    {
        if (!impacted) { impacted = true; audioSource.clip = explosionClip; audioSource.Play(); Explode(); }
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Rigidbody rigidbody))
            {
            }

            if (colliders[i].TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(100);
                Rigidbody[] rbs = enemy.ragdoll.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                {
                    Vector3 direction = (colliders[i].transform.position - transform.position).normalized;
                    rb.AddForce(direction * power, ForceMode.Impulse);
                }
            }
        }
        GetComponent<Rigidbody>().isKinematic = true;
        model.SetActive(false);
        explosion.SetActive(true);

        Destroy(gameObject, 1);
    }
}
