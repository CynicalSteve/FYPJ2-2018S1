using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour {

    public void ToGame()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
