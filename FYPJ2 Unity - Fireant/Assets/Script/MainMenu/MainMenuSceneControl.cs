using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneControl : MonoBehaviour {

    public enum MAIN_MENU_SCREEN
    {
        SCREEN_SPLASHSCREEN,
        SCREEN_MAINMENU,
        SCREEN_LEVEL_SELECT,
        SCREEN_OPTIONS,
        SCREEN_CONTROLS,

        TOTAL_SCREENS_NUM
    }

    MAIN_MENU_SCREEN currentMenuScreen;
    GameObject[] ScreenList = new GameObject[(short)MAIN_MENU_SCREEN.TOTAL_SCREENS_NUM];

	// Use this for initialization
	void Start () {
        currentMenuScreen = MAIN_MENU_SCREEN.SCREEN_SPLASHSCREEN;

        //Declare the gameobject screens and put them into the array
        ScreenList[(short)MAIN_MENU_SCREEN.SCREEN_SPLASHSCREEN] = GameObject.FindGameObjectWithTag("SplashScreen");

        ScreenList[(short)MAIN_MENU_SCREEN.SCREEN_MAINMENU] = GameObject.FindGameObjectWithTag("MainScreen");
        ScreenList[(short)MAIN_MENU_SCREEN.SCREEN_MAINMENU].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(currentMenuScreen == MAIN_MENU_SCREEN.SCREEN_SPLASHSCREEN && Input.anyKeyDown)
        {
            ChangeToMainScreen();
        }
	}

    public MAIN_MENU_SCREEN GetCurrentScreen()
    {
        return currentMenuScreen;
    }

    //Screen Transition
    void ChangeScreen(MAIN_MENU_SCREEN currentScreen, MAIN_MENU_SCREEN newScreen)
    {
        ScreenList[(short)currentScreen].SetActive(false);
        ScreenList[(short)newScreen].SetActive(true);
    }

    //Functions for buttons
    public void ChangeToMainScreen()
    {
        ChangeScreen(currentMenuScreen, MAIN_MENU_SCREEN.SCREEN_MAINMENU);
        currentMenuScreen = MAIN_MENU_SCREEN.SCREEN_MAINMENU;
    }

    public void ChangeToLevelScreen()
    {
        ChangeScreen(currentMenuScreen, MAIN_MENU_SCREEN.SCREEN_LEVEL_SELECT);
        currentMenuScreen = MAIN_MENU_SCREEN.SCREEN_LEVEL_SELECT;
    }

    public void ChangeToOptionsScreen()
    {
        ChangeScreen(currentMenuScreen, MAIN_MENU_SCREEN.SCREEN_OPTIONS);
        currentMenuScreen = MAIN_MENU_SCREEN.SCREEN_OPTIONS;
    }

    public void ChangeToControlsScreen()
    {
        ChangeScreen(currentMenuScreen, MAIN_MENU_SCREEN.SCREEN_CONTROLS);
        currentMenuScreen = MAIN_MENU_SCREEN.SCREEN_CONTROLS;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
