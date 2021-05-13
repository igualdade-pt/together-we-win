﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_SM : MonoBehaviour
{

    private StartMenuManager startMenuManager;

    private AudioManager audioManager;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private Image soundImage;

    [SerializeField]
    private Sprite[] spritesOnOffSound;

    [SerializeField]
    private GameObject buttonCloseBooksPanel;

    [SerializeField]
    private GameObject[] buttonBookSelectedPanel;


    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject booksPanel;

    [SerializeField]
    private GameObject allBooksPanel;

    [SerializeField]
    private GameObject buttonsBooksPanel;

    [SerializeField]
    private GameObject gamePanel;


    private int indexBookSelected;

    private bool isSoundActive = true;

    private void Awake()
    {
        informationPanel.SetActive(false);
        booksPanel.SetActive(false);
        gamePanel.SetActive(false);
        soundImage.sprite = spritesOnOffSound[0];

        for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
        {
            buttonBookSelectedPanel[i].SetActive(false);
        }
    }

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
        //audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        isSoundActive = true;
    }

    public void _StartButtonClicked (int indexScene)
    {
        Debug.Log("Start Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadAsyncScene(indexScene);
    }

    public void _InformationButtonClicked ()
    {
        if (!informationPanel.activeSelf)
        {
            informationPanel.SetActive(true);
        }
    }

    public void _CloseInformationButtonClicked()
    {
        if (informationPanel.activeSelf)
        {
            informationPanel.SetActive(false);
        }
    }

    public void _LanguageButtonClicked (int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);

        startMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked()
    {
        if (!booksPanel.activeSelf)
        {
            allBooksPanel.SetActive(false);
            booksPanel.SetActive(true);
            buttonsBooksPanel.SetActive(true);
        }
    }

    public void _CloseBooksButtonClicked()
    {
        if (booksPanel.activeSelf)
        {
            booksPanel.SetActive(false);
        }
    }

    public void _BookButtonSelectedClicked(int indexBook)
    {
        if (booksPanel.activeSelf)
        {
            buttonsBooksPanel.SetActive(false);
            buttonCloseBooksPanel.SetActive(false);

            for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
            {
                if (i == indexBook)
                {
                    buttonBookSelectedPanel[i].SetActive(true);
                    allBooksPanel.SetActive(true);
                    indexBookSelected = indexBook;
                }
            }
        }
    }

    public void _CloseBookButtonSelectedClicked()
    {
        if (booksPanel.activeSelf)
        {
            allBooksPanel.SetActive(false);
            buttonBookSelectedPanel[indexBookSelected].SetActive(false);
            buttonsBooksPanel.SetActive(false);
            buttonsBooksPanel.SetActive(true);
            buttonCloseBooksPanel.SetActive(true);
        }
    }


    public void _GamesButtonClicked()
    {
        if (!gamePanel.activeSelf)
        {
            gamePanel.SetActive(true);
        }
    }

    public void _CloseGamesButtonClicked()
    {
        if (gamePanel.activeSelf)
        {
            gamePanel.SetActive(false);
        }
    }

    public void _SoundButtonClicked()
    {
        if (isSoundActive)
        {
            //soundButton.image.sprite = spriteOffOnSound[0];
            Debug.Log("sound is OFF, value:" + isSoundActive);
            //audioManager.SetVolume(isSoundActive);
            isSoundActive = false;
            soundImage.sprite = spritesOnOffSound[1];
        }
        else
        {
            //soundButton.image.sprite = spriteOffOnSound[1];
            Debug.Log("sound is ON, value:" + isSoundActive);
            //audioManager.SetVolume(isSoundActive);
            isSoundActive = true;
            soundImage.sprite = spritesOnOffSound[0];
        }
    }

    public void UpdateLanguage(int indexLanguage)
    {
        
    }

}
