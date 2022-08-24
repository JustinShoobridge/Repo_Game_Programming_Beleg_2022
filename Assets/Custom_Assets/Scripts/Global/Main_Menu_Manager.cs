using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    public void onButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void onButtonQuit()
    {
        Application.Quit();
    }
}
