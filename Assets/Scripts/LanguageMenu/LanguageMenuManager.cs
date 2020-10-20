using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageMenuManager : MonoBehaviour
{
    private SystemLanguage languageSystem;

    private int indexLanguage;

    private UIManager_LM uiManager_LM;

    [SerializeField]
    private int indexSceneToLoad;

    void Start()
    {
        // Orientation Screen
        Screen.orientation = ScreenOrientation.Portrait;

        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;

        Screen.orientation = ScreenOrientation.AutoRotation;

        // Language
        languageSystem = Application.systemLanguage;

        switch (languageSystem)
        {           
            case SystemLanguage.English:
                Debug.Log("System language: " + languageSystem);
                indexLanguage = 0;
                break;
            case SystemLanguage.Italian:
                Debug.Log("System language: " + languageSystem);
                indexLanguage = 1;
                break;
            case SystemLanguage.Portuguese:
                Debug.Log("System language: " + languageSystem);
                indexLanguage = 2;
                break;            
            case SystemLanguage.Spanish:
                Debug.Log("System language: " + languageSystem);
                indexLanguage = 3;
                break;
            case SystemLanguage.Swedish:
                Debug.Log("System language: " + languageSystem);
                indexLanguage = 4;
                break;
           
            default:
                Debug.Log("Unavailable language: " + languageSystem);
                indexLanguage = 0;
                break;
        }

        uiManager_LM = FindObjectOfType<UIManager_LM>().GetComponent<UIManager_LM>();

        uiManager_LM.UpdateFlag(indexLanguage);
        uiManager_LM.ChangeCurrentIndexFlag = indexLanguage;
    }

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("languageSystem",indexLanguage);
        Debug.Log("Index Language saved: " + PlayerPrefs.GetInt("languageSystem", 0));

        SceneManager.LoadScene(indexSceneToLoad);

    }

    /// <summary>
    /// For Loading
    /// </summary>
    /*private IEnumerator LoadAsyncScene()
    {
         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexSceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }*/


    public int ChangeLanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }
}
