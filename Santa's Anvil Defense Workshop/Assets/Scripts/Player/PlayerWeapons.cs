using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject[] weapons;

    int currentWeaponIndex;

    private void Start()
    {
        SwitchWeapons(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapons(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapons(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapons(2);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SwitchWeapons(10);
            Debug.Log("Up");
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SwitchWeapons(-10);
            Debug.Log("Down");
        }
    }

    private void SwitchWeapons(int id)
    {
        if (id == 10)
        {
            if (currentWeaponIndex == 2)
            {
                currentWeaponIndex = 0;
            }
            else { currentWeaponIndex++; }
        }
        else if (id == -10)
        {
            if (currentWeaponIndex == 0)
            {
                currentWeaponIndex = 2;
            }
            else { currentWeaponIndex--; }
        }
        else
        {
            currentWeaponIndex = id;
        }

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
