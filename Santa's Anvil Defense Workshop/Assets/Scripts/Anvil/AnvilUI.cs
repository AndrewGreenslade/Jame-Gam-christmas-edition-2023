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
    public Color selectedColor;
    public Color defaultColor;
    public Color disabledColor;

    bool open;

    public void CheckToys()
    {
        foreach (GameObject go in toySelections)
        {
            go.GetComponent<Button>().interactable = false;
            go.GetComponent<Image>().color = disabledColor;
        }

        var io = GameManager.Instance.Anvil.ToysToCraft;
        foreach (ToyToCraft t in io)
        {
            if (t.current)
            {
                continue;
            }
            toySelections[t.ID].GetComponent<Button>().interactable = true;
            toySelections[t.ID].GetComponent<Image>().color = defaultColor;
        }
    }

    public void UpdateToyList()
    {
        var toys = GameManager.Instance.Anvil.ToysToCraft;
        for (int i = 0; i < toys.Count; i++)
        {
            if (toys[i].GameObject == null)
            {
                GameObject temp = Instantiate(toyToCraftUIPrefab, toyToCraftParent.transform);
                toys[i].GameObject = temp;
            }
            toys[i].UpdateUI();
            if (!toys[i].GameObject.activeSelf)
            {
                Destroy(toys[i].GameObject);
                toys.Remove(toys[i]);
            }
        }
        CheckToys();
    }

    private void Update()
    {
        if (countdown != null)
        {
            countdown.text = GameManager.Instance.Anvil.Countdown;
        }
        if (open && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            HideUI();
        }
    }

    public void ToySelected(int toyID)
    {
        HideUI();
        var io = GameManager.Instance.Anvil.ToysToCraft;
        foreach (ToyToCraft t in io)
        {
            if (t.ID == toyID && !t.current)
            {
                t.MakeCurrent();
                return;
            }
        }
    }

    public void HideUI()
    {
        toySelectionUI.SetActive(false);
        BaseFirstPersonController.Instance.mouseLook.SetCursorLock(true);
        BaseFirstPersonController.Instance.pause = false;
        open = false;
    }

    public void ShowUI()
    {
        CheckToys();
        toySelectionUI.SetActive(true);
        BaseFirstPersonController.Instance.pause = true;
        BaseFirstPersonController.Instance.mouseLook.SetCursorLock(false);
        open = true;
    }
}
