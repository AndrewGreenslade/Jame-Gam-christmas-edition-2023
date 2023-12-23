using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnvilUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI countdown;
    [SerializeField]
    Image healthBar;
    public GameObject toySelectionUI;
    public GameObject[] toySelections;
    public GameObject toyToCraftUIPrefab;
    public GameObject toyToCraftParent;
    public Sprite[] toyIcons;
    public Color selectedColor;
    public Color defaultColor;
    public Color disabledColor;

    float openTime;
    bool open;

    public bool isOpen { get { return open; } }

    private void Start()
    {
        GameManager.Instance.anvilUI = this;
    }

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
        if (GameManager.Instance.isGameOver) { return; }
        healthBar.fillAmount = GameManager.Instance.Anvil.health / 100f;
        if (countdown != null)
        {
            countdown.text = GameManager.Instance.Anvil.Countdown;
        }
        if (
            open
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            && openTime < Time.time
        )
        {
            HideUI();
        }
        if (open)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ToySelected(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ToySelected(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ToySelected(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ToySelected(3);
            }
        }
    }

    public void ToySelected(int toyID)
    {
        if (toySelections[toyID].GetComponent<Button>().interactable == false) { return; }
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
        open = true;
        openTime = Time.time + 1;
        CheckToys();
        toySelectionUI.SetActive(true);
        BaseFirstPersonController.Instance.pause = true;
        BaseFirstPersonController.Instance.mouseLook.SetCursorLock(false);
    }
}
