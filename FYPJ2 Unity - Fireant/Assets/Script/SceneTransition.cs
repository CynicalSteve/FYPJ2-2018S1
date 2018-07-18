using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SceneTransition : MonoBehaviour
{
    int lol1 = 0;
    public void ToGame()
    {
        if (PlayerPrefs.GetFloat("savecheckpoint") >= 1)
        {
            lol1++;
            PlayerPrefs.SetFloat("respawntocheckpoint", lol1);
            SceneManager.LoadScene("Main Scene");
        }
        else
            SceneManager.LoadScene("Main Scene");
    }
    public void ToAbout()
    {
        SceneManager.LoadScene("about");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ToCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
