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
            SceneManager.LoadScene(PlayerPrefs.GetString("lastloadedscene"));
        }
        else
            SceneManager.LoadScene(PlayerPrefs.GetString("lastloadedscene"));
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
    public void ToOption()
    {
        SceneManager.LoadScene("option");
    }
    public void Tolevelone()
    {
        SceneManager.LoadScene("levelone");
    }
    public void Toleveltwo()
    {
        SceneManager.LoadScene("leveltwo");
    }
    public void Tolevelthree()
    {
        SceneManager.LoadScene("levelthree");
    }
    public void Tolevelfour()
    {
        SceneManager.LoadScene("levelfour");
    }
    public void Tolevelfive()
    {
        SceneManager.LoadScene("levelfive");
    }
    public void Tolevelend()
    {
        SceneManager.LoadScene("levelend");
    }
    public void Tolevelselect()
    {
        SceneManager.LoadScene("levelselect");
    }
}
