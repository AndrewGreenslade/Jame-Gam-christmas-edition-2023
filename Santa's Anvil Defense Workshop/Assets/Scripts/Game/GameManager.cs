using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int CountdownTime;
    Anvil anvil;

    int kills = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int Kills
    {
        get { return kills; }
    }

    public Anvil Anvil
    {
        get { return anvil; }
        set { anvil = value; }
    }
}
