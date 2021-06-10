using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    private AudioManager audioManager;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private GameObject[] levelButtons;

    [SerializeField]
    private Image guideImage;

    [SerializeField]
    private Sprite[] guideSprites;

    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject guidePanel;

    private int indexLevelSelected = -1;

    private void Awake()
    {
        loadingPanel.SetActive(false);
        guidePanel.SetActive(false);
    }

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void _SettingsButtonClicked(int indexScene)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        mainMenuManager.LoadScene(indexScene);
    }

    public void _LevelButtonClicked(int indexLevel)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        indexLevelSelected = indexLevel;

        switch (indexLevelSelected)
        {
            case 0:
                guideImage.sprite = guideSprites[0];
                guidePanel.SetActive(true);
                break;
            case 3:
                guideImage.sprite = guideSprites[1];
                guidePanel.SetActive(true);
                break;
            default:
                loadingPanel.SetActive(true);
                mainMenuManager.LoadAsyncGamePlay(indexLevelSelected);
                break;
        }

    }

    public void _GuideButtonClicked()
    {
        loadingPanel.SetActive(true);
        mainMenuManager.LoadAsyncGamePlay(indexLevelSelected);
    }

    public void UpdateLanguage(int indexLanguage)
    {

    }

    public void UpdadeLevelButtons(int unlockedLevels)
    {
        // Set All Buttons Lock
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }

        // Set Only the Buttons Unlocked
        int unlock = unlockedLevels + 1;
        Mathf.Clamp(unlock, 0, levelButtons.Length);
        Debug.Log("Unlocked Levels:  " + unlock);
        for (int j = 0; j < unlock; j++)
        {
            levelButtons[j].SetActive(true);
            if (j >= unlock - 1)
            {
                LeanTween.scale(levelButtons[j], levelButtons[j].transform.localScale * 1.2f, 0.5f).setLoopPingPong();
            }
        }
    }
}
