using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_LM : MonoBehaviour
{
    [SerializeField]
    private Image flagImage;

    [SerializeField]
    private Sprite[] flags;

    [SerializeField]
    private Text textLanguage;

    [SerializeField]
    private Button buttonRight;

    private LanguageMenuManager languageMenuManager;

    private int currentIndexFlag;

    private void Start()
    {
       languageMenuManager = FindObjectOfType<LanguageMenuManager>().GetComponent<LanguageMenuManager>();
    }


    public void UpdateFlag(int indexLanguage)
    {
        // Change Flag
        flagImage.sprite = flags[Mathf.Clamp(indexLanguage, 0, flags.Length - 1)];

        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "Language";
                break;

            case 1:
                // Italian
                textLanguage.text = "Linguaggio";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Idioma";
                break;

            case 3:
                // Spanish
                textLanguage.text = "Idioma";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Språk";
                break;

            default:
                // English
                textLanguage.text = "Language";
                break;
        }
    }


    public void _RightButtonClick()
    {
        if (currentIndexFlag < flags.Length - 1)
        {
            currentIndexFlag++;
        }
        else
        {
            currentIndexFlag = 0;
        }
        

        Debug.Log("Current Index Flag: " + currentIndexFlag);
        UpdateFlag(currentIndexFlag);
        languageMenuManager.ChangeLanguageIndex = currentIndexFlag;
    }

    public void _LeftButtonClick()
    {
        if (currentIndexFlag > 0)
        {
            currentIndexFlag--;
        }
        else
        {
            currentIndexFlag = flags.Length - 1;
        }

        Debug.Log("Current Index Flag: " + currentIndexFlag);
        UpdateFlag(currentIndexFlag);
        languageMenuManager.ChangeLanguageIndex = currentIndexFlag;
    }

    public void _ConfirmButton()
    {
        languageMenuManager.LoadLevel();    
    }

    public int ChangeCurrentIndexFlag
    {
        set { currentIndexFlag = value; }
    }

}
