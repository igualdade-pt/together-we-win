using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private AudioManager audioManager;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject gameEndedPanel;

    [SerializeField]
    private GameObject characterEndPanel;

    [SerializeField]
    private GameObject sadFace_L13;

    [SerializeField]
    private GameObject sadFace_L24;

    [SerializeField]
    private GameObject happyFace_L13;

    [SerializeField]
    private GameObject happyFace_L24;

    [Header("Texts")]
    [Space]
    [SerializeField]
    private Text textTitleGameEnded;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        loadingPanel.SetActive(false);
        gameEndedPanel.SetActive(false);
        characterEndPanel.SetActive(false);
        happyFace_L13.SetActive(false);
        happyFace_L24.SetActive(false);
        sadFace_L13.SetActive(false);
        sadFace_L24.SetActive(false);
    }

    public void UpdateLanguage(int indexLanguage)
    {
        switch (indexLanguage)
        {
            case 0:
                // EN
                textTitleGameEnded.text = "Play Again?";
                break;

            case 1:
                // IT
                textTitleGameEnded.text = "Gioca Di Nuovo?";
                break;

            case 2:
                // PT
                textTitleGameEnded.text = "Jogar De Novo?";
                break;

            case 3:
                // ES
                textTitleGameEnded.text = "¿Juega DSe Nuevo?";
                break;

            case 4:
                // SE
                textTitleGameEnded.text = "Spela Igen?";
                break;

            default:
                // EN
                textTitleGameEnded.text = "Play Again?";
                Debug.Log("UiManager_GM Menu, Unavailable language, English Selected: " + indexLanguage);
                break;
        }
    }

    public void _RestartButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        var x = SceneManager.GetActiveScene();
        gameplayManager.LoadSelectedScene(x.buildIndex);
    }


    public void _Return()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        gameplayManager.LoadSelectedScene(3);
    }

    public void SetGameEndedPanel(bool value)
    {
        gameEndedPanel.SetActive(value);
    }

    public void GameEnded(int level, bool won)
    {

        Debug.Log(level);
        Debug.Log(won);

        characterEndPanel.SetActive(true);
        if (level == 0 || level == 2)
        {
            if (won)
            {
                happyFace_L13.SetActive(true);
            }
            else
            {
                sadFace_L13.SetActive(true);
            }
        }
        else if (level == 1 || level == 3)
        {
            if (won)
            {
                happyFace_L24.SetActive(true);
            }
            else
            {
                sadFace_L24.SetActive(true);
            }
        }
        StartCoroutine(ShowPanelGameEnd());
    }

    private IEnumerator ShowPanelGameEnd()
    {
        yield return new WaitForSeconds(2f);

        gameEndedPanel.SetActive(true);
    }

    public void _PlayButtonSound()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
    }
}
