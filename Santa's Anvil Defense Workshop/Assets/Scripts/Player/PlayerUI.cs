using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        ammoText.text = PlayerWeapons.Instance.GetCurrentWeapon().GetComponent<Weapon>()?.GetAmmo();
    }
}
