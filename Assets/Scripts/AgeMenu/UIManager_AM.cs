using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_AM : MonoBehaviour
{
    private AgeMenuManager ageMenuManager;
    private AudioManager audioManager;

    [SerializeField]
    private Text textLanguage;

    private void Start()
    {
        ageMenuManager = FindObjectOfType<AgeMenuManager>().GetComponent<AgeMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void UpdateLanguage(int indexLanguage)
    {
        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textLanguage.text = "How Old Are You?";
                break;

            case 1:
                // Italian
                textLanguage.text = "Quanti Anni Hai?";
                break;

            case 2:
                // Portuguese
                textLanguage.text = "Quantos Anos Tens?";
                break;

            case 3:
                // Spanish
                textLanguage.text = "¿Cuantos Años Tienes?";
                break;

            case 4:
                // Swedish
                textLanguage.text = "Hur Gammal Är Du?";
                break;

            default:
                // English
                textLanguage.text = "How Old Are You?";
                break;
        }

    }

    public void _AgeButton(int age)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        ageMenuManager.AgeButtonClicked(age);
    }
}
