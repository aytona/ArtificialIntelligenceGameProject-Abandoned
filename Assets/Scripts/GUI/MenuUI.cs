using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuUI : MonoBehaviour {

    public GameObject HowTo;
    public GameObject MainMenu;

    public void doStart()
    {
        Application.LoadLevel(1);
    }

    public void doHowTo()
    {
        MainMenu.SetActive(false);
        HowTo.SetActive(true);
    }

    public void doQuit()
    {
        Application.Quit();
    }

    public void doMenu()
    {
        MainMenu.SetActive(true);
        HowTo.SetActive(false);
    }
}
