using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    private AudioManager audioManager;

    private GameInstanceScript gameInstance;

    [Header("Buttons")]
    [Space]
    [SerializeField]
    private GameObject[] levelButtons;


    [Header("Panels")]
    [Space]
    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject guidePanel;

    private int indexLevelSelected = -1;

    [Header("Texts")]
    [Space]
    [SerializeField]
    private Text textTitle;

    [SerializeField]
    private Image guideImage;

    [SerializeField]
    private Image guideImageLvl4;

    [SerializeField]
    private Sprite [] spriteGuide;

    [SerializeField]
    private Sprite [] spriteGuideLvl4;

    [SerializeField]
    private string[] textButtonEN;

    [SerializeField]
    private string[] textButtonIT;

    [SerializeField]
    private string[] textButtonPT;

    [SerializeField]
    private string[] textButtonES;

    [SerializeField]
    private string[] textButtonSE;

    private void Awake()
    {
        loadingPanel.SetActive(false);
        guidePanel.SetActive(false);

        guideImage.gameObject.SetActive(false);
        guideImageLvl4.gameObject.SetActive(false);

        guideImage.sprite = spriteGuide[0];
        guideImageLvl4.sprite = spriteGuideLvl4[0];
    }

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

        if (gameInstance.UnlockNewLevel)
        {
            // Play Sound
            audioManager.PlayClip(5, 0.3f);
            // ****
        }

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
                guideImage.gameObject.SetActive(true);
                guidePanel.SetActive(true);
                break;
            case 3:
                guideImageLvl4.gameObject.SetActive(true);
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
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        mainMenuManager.LoadAsyncGamePlay(indexLevelSelected);
    }

    public void UpdateLanguage(int indexLanguage)
    {
        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textTitle.text = "Together we win!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonEN[i];
                }
                break;

            case 1:
                // Italian
                textTitle.text = "Insieme vinciamo!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonIT[i];
                }
                break;

            case 2:
                // Portuguese
                textTitle.text = "Juntos vencemos!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonPT[i];
                }
                break;

            case 3:
                // Spanish
                textTitle.text = "¡Juntos ganamos!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonES[i];
                }
                break;

            case 4:
                // Swedish
                textTitle.text = "Tillsammans vinner vi!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonSE[i];
                }
                break;

            default:
                // English
                textTitle.text = "Together we win!";

                for (int i = 0; i < levelButtons.Length; i++)
                {
                    levelButtons[i].GetComponentInChildren<Text>().text = textButtonEN[i];
                }
                break;
        }

        guideImage.sprite = spriteGuide[indexLanguage];
        guideImageLvl4.sprite = spriteGuideLvl4[indexLanguage];
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
