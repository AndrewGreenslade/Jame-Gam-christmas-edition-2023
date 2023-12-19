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
    bool current;
    int totalRequired;
    int currentKills;
    Image icon;
    TextMeshProUGUI text;

    public ToyToCraft(int _id)
    {
        id = _id;
    }

    public GameObject GameObject
    {
        get { return go; }
        set { go = value; }
    }

    public int ID
    {
        get { return id; }
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
}