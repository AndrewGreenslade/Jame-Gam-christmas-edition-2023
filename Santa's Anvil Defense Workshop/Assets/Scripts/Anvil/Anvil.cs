using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToyToCraft
{
    int id;
    GameObject go;
    public bool current { get; private set; }
    int totalRequired;
    int currentKills;
    int killsAtTimeOfSettingCurrent;
    Image background;
    Image icon;
    TextMeshProUGUI text;

    public ToyToCraft(int _id)
    {
        id = _id;
    }

    public GameObject GameObject
    {
        get { return go; }
        set
        {
            go = value;
            background = value.GetComponent<Image>();
            icon = value.GetComponentsInChildren<Image>()[1];
            text = value.GetComponentInChildren<TextMeshProUGUI>();
            text.gameObject.SetActive(false);
            icon.sprite = GameManager.Instance.Anvil.UI.toyIcons[id];
        }
    }

    public int ID
    {
        get { return id; }
    }

    public void MakeCurrent()
    {
        current = true;
        killsAtTimeOfSettingCurrent = GameManager.Instance.kills;
        totalRequired = Random.Range(2, 10);
        background.color = GameManager.Instance.Anvil.UI.selectedColor;
        text.text = string.Format("{0}/{1}", currentKills, totalRequired);
        text.gameObject.SetActive(true);
    }

    public void UpdateUI()
    {
        if (!current)
        {
            return;
        }
        currentKills = GameManager.Instance.kills - killsAtTimeOfSettingCurrent;

        if (currentKills >= totalRequired)
        {
            go.SetActive(false);
        }

        text.text = string.Format("{0}/{1}", currentKills, totalRequired);
    }
}

public class Anvil : MonoBehaviour
{
    [SerializeField]
    List<ToyToCraft> toysToCraft = new List<ToyToCraft>();
    public AnvilUI UI;

    float countdown;

    private void Start()
    {
        GameManager.Instance.Anvil = this;
        countdown = GameManager.Instance.CountdownTime;
    }

    private void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = GameManager.Instance.CountdownTime;
            toysToCraft.Add(new ToyToCraft(Random.Range(0, 3)));
            UI.UpdateToyList();
        }
    }

    public List<ToyToCraft> ToysToCraft
    {
        get { return toysToCraft; }
    }

    public string Countdown
    {
        get { return string.Format("{0:00}s", Mathf.FloorToInt(countdown % 60)); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Rigidbody>().isKinematic)
            {
                return;
            }
            PopupManager.Instance.ShowPickupMessage("E", KeyCode.E, UI.ShowUI);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PopupManager.Instance.HidePickupMessage();
        }
    }
}
