using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject[] weapons;

    int currentWeaponIndex;

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i != currentWeaponIndex)
            {
                weapons[i].SetActive(false);
            }
            else
            {
                weapons[i].SetActive(true);
            }
        }
    }
}
