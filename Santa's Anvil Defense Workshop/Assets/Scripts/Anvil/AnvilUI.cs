using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnvilUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countdown;
    public GameObject toySelectionUI;
    public GameObject[] toySelections;
    public GameObject toyToCraftUIPrefab;
    public GameObject toyToCraftParent;
    public Sprite[] toyIcons;

    public int currentToy; //0 - Car, 1 - Doll, 2 - Tricycle, 3 - Duck

    public void CheckToys() { }

    public void UpdateToyList()
    {
        var io = GameManager.Instance.Anvil.ToysToCraft;
        foreach (ToyToCraft t in io)
        {
            if (t.GameObject == null)
            {
                GameObject temp = Instantiate(toyToCraftUIPrefab, toyToCraftParent.transform);
                t.GameObject = temp;
            }
        }
    }

    private void Update()
    {
        if (countdown != null)
        {
            countdown.text = GameManager.Instance.Anvil.Countdown;
        }
    }

    public void ToySelected(int toyID)
    {
        currentToy = toyID;
        HideUI();
    }

    public void HideUI()
    {
        toySelectionUI.SetActive(false);
        BaseFirstPersonController.Instance.pause = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void ShowUI()
    {
        toySelectionUI.SetActive(true);
        BaseFirstPersonController.Instance.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
