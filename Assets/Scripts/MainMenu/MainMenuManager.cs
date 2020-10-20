using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //public AspectRatio AspectRatio { get; private set; }

    private int indexLanguage;

    private UIManager_MM uiManager_MM;

    private void Start()
    {
        // Orientation Screen
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;


        // Attribute Language      
        indexLanguage = PlayerPrefs.GetInt("languageSystem", 0);

        
        switch (indexLanguage)
        {
            case 0:
                Debug.Log("Main Menu, System language English: " + indexLanguage);

                break;
            case 1:
                Debug.Log("Main Menu, System language Italian: " + indexLanguage);

                break;
            case 2:
                Debug.Log("Main Menu, System language Portuguese: " + indexLanguage);

                break;
            case 3:
                Debug.Log("Main Menu, System language Spanish: " + indexLanguage);

                break;
            case 4:
                Debug.Log("Main Menu, System language Swedish: " + indexLanguage);
                break;

            default:
                Debug.Log("Main Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }

        uiManager_MM = FindObjectOfType<UIManager_MM>().GetComponent<UIManager_MM>();

    }


    private void Update()
    {

    }

    public void LoadScene(int indexScene, int indexLevel)
    {
        PlayerPrefs.SetInt("indexLevel", indexLevel);
        Debug.Log("Scene Index = " + indexScene);
    }
    
}