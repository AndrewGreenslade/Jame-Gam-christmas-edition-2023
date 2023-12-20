using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [SerializeField]
    TextMeshProUGUI interactText;
    string defaultInteractText;

    KeyCode currentKeyCode;
    Action currentAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        interactText.gameObject.SetActive(false);
        defaultInteractText = interactText.text;
    }

    private void Update()
    {
        if (currentAction != null)
        {
            if (Input.GetKeyDown(currentKeyCode))
            {
                currentAction.Invoke();
                currentKeyCode = KeyCode.None;
                currentAction = null;
                HidePickupMessage();
            }
        }
    }

    public void ShowPickupMessage(string key, KeyCode code, Action action)
    {
        if (interactText.gameObject.activeSelf)
        {
            return;
        }
        interactText.gameObject.SetActive(true);
        interactText.text = string.Format(defaultInteractText, key);
        currentKeyCode = code;
        currentAction = action;
    }

    public void HidePickupMessage()
    {
        interactText.gameObject.SetActive(false);
    }
}
