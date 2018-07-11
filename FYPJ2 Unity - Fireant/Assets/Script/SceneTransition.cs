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
            SceneManager.LoadScene("YongHan Scene");
            lol1++;
            PlayerPrefs.SetFloat("respawntocheckpoint", lol1);
        }
        else
            SceneManager.LoadScene("YongHan Scene");
    }
}
