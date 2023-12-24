using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject aboutScreen;
    public GameObject playScreen;
    public GameObject mainScreen;

    public GameObject[] cameras;
    int currentCamIndex;

    private void Start()
    {
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
        playScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(SwitchCamera());

    }

    IEnumerator SwitchCamera()
    {
        yield return new WaitForSeconds(5);
        cameras[currentCamIndex].SetActive(false);

        if (currentCamIndex == cameras.Length) { currentCamIndex = 0; }
        else
        {
            currentCamIndex++;
        }
        cameras[currentCamIndex].SetActive(true);

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayButton()
    {
        mainScreen.SetActive(false);
        playScreen.SetActive(true);
    }

    public void AboutButton()
    {
        mainScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void BackButton()
    {
        mainScreen.SetActive(true);
        aboutScreen.SetActive(false);
        playScreen.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
