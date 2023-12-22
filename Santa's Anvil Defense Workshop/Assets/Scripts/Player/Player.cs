using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    public void DealDamage(int damage)
    {
        health -= damage;
    }
}
