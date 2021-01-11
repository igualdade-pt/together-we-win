﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_SM : MonoBehaviour
{

    private StartMenuManager startMenuManager;

    [SerializeField]
    private GameObject informationPanel;

    [SerializeField]
    private GameObject booksPanel;

    [SerializeField]
    private GameObject buttonsBooksPanel;

    [SerializeField]
    private GameObject buttonCloseBooksPanel;

    [SerializeField]
    private GameObject allBooksPanel;

    [SerializeField]
    private GameObject[] buttonBookSelectedPanel;

    private int indexBookSelected;

    private void Awake()
    {
        informationPanel.SetActive(false);
        booksPanel.SetActive(false);

        for (int i = 0; i < buttonBookSelectedPanel.Length; i++)
        {
            buttonBookSelectedPanel[i].SetActive(false);
        }
    }

    private void Start()
    {
        startMenuManager = FindObjectOfType<StartMenuManager>().GetComponent<StartMenuManager>();
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

    public void _SoundButtonClicked()
    {

    }

    public void UpdateLanguage(int indexLanguage)
    {
        
    }

}
