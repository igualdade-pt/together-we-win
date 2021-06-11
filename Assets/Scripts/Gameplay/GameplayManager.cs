using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    private GameInstanceScript gameInstance;

    private AudioManager audioManager;

    private MusicManagerScript musicManager;

    private UIManager_GM uiManager_GM;

    private int indexLevel = 0;

    [SerializeField]
    private GameObject[] Levels;

    [SerializeField]
    private int indexSceneToLoad = 2;

    [SerializeField]
    private GameObject [] handSObject;

    [SerializeField]
    private Animation [] handSAnimation;

    [Header("Light")]
    [SerializeField]
    private Light2D globalLight;

    [SerializeField]
    private Light2D playersLight;


    [SerializeField]
    private Color globalLightColor;

    [SerializeField]
    private GameObject lampLight;

    private bool doOnce = true;

    private bool gameStarted = false;


    private void Start()
    {
        doOnce = true;

        handSObject[0].SetActive(true);
        handSObject[1].SetActive(true);
        StartCoroutine(HandCoroutine());

        // Attribute Language 
        gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();
        switch (gameInstance.LanguageIndex)
        {
            case 0:
                Debug.Log("Gameplay, System language English");

                break;
            case 1:
                Debug.Log("Gameplay, System language Italian");

                break;
            case 2:
                Debug.Log("Gameplay, System language Portuguese");

                break;
            case 3:
                Debug.Log("Gameplay, System language Spanish");

                break;
            case 4:
                Debug.Log("Gameplay, System language Swedish");
                break;

            default:
                Debug.Log("Gameplay, Unavailable language, English Selected");

                break;
        }

        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();

        uiManager_GM.UpdateLanguage(gameInstance.LanguageIndex);

        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        // Get Level Selected
        indexLevel = gameInstance.LevelIndex;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].activeSelf)
            {
                Levels[i].SetActive(false);
            }
        }

        switch (indexLevel)
        {
            // Level 1
            case 0:
                Levels[0].SetActive(true);

                Debug.Log("Level: " + 1);
                break;

            // Level 2
            case 1:
                Levels[1].SetActive(true);

                Debug.Log("Level: " + 2);
                break;

            // Level 3
            case 2:
                Levels[2].SetActive(true);

                Debug.Log("Level: " + 3);
                break;

            // Level 4
            case 3:
                Levels[3].SetActive(true);

                Debug.Log("Level: " + 4);
                break;

            // Level 1
            default:
                Debug.Log("Error Getting Level: " + indexLevel + 1);
                Levels[0].SetActive(true);
                break;
        }

        switch (gameInstance.DifficultyLevelIndex)
        {
            // Difficulty Basic
            case 0:


                Debug.Log("Level Difficulty Basic:  " + gameInstance.DifficultyLevelIndex);
                break;

            // Difficulty Expert
            case 1:


                Debug.Log("Level Difficulty Expert: " + gameInstance.DifficultyLevelIndex);
                break;

            default:
                Debug.Log("Error Getting Difficulty Level: " + gameInstance.DifficultyLevelIndex + "; Default Difficulty: Basic");

                break;
        }

        musicManager = FindObjectOfType<MusicManagerScript>().GetComponent<MusicManagerScript>();

        musicManager.LowMusic();
    }

    private IEnumerator HandCoroutine ()
    {
        while (!gameStarted)
        {
            handSAnimation[0].Play();
            handSAnimation[1].Play();
            yield return new WaitForSeconds(handSAnimation[0].clip.length + 3f);
        }
    }

    public void GameStarted(Player_S player)
    {
        if (!gameStarted)
        {
            gameStarted = true;

            for (int i = 0; i < handSObject.Length; i++)
            {
                handSObject[i].SetActive(false);
            }
            StopCoroutine(HandCoroutine());

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            Camera camera = Camera.main;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Player_S>() != null && players[i] != player)
                {
                    players[i].GetComponent<Player_S>().GameStarted();
                }
            }

            for (int i = 0; i < enemies.Length; i++)
            {               
                if (enemies[i] != null)
                {                    
                    enemies[i].GetComponent<Enemy_S>().GameStarted();
                }
            }

            camera.GetComponent<Camera_S>().GameStarted();

            Debug.Log("GameStarted");
        }
    }


    public void GameEnded(bool won)
    {
        if (doOnce)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            doOnce = false;
            if (won)
            {

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<Player1_S>() != null)
                    {
                        players[i].GetComponent<Player1_S>().SetSpeed(0);
                    }
                    else
                    {
                        players[i].GetComponent<Player_S>().SetSpeed(0);
                    }
                }


                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        enemies[i].GetComponent<Enemy_S>().GameEnded(won);
                    }
                }

                if (indexLevel == 0)
                {
                    playersLight.intensity = 0;
                    lampLight.SetActive(true);
                    globalLight.intensity = 0.8f;
                    globalLight.color = globalLightColor;
                }
                uiManager_GM.GameEnded(indexLevel, won);

                indexLevel++;
                if (indexLevel > PlayerPrefs.GetInt("unlockedLevels", 0))
                {
                    int index = Mathf.Clamp(indexLevel, 0, 3);
                    Debug.Log("WON, Levels Locked: " + index);
                    PlayerPrefs.SetInt("unlockedLevels", index);
                }


            }
            else
            {

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i] != null)
                    {
                        if (players[i].GetComponent<Player1_S>() != null)
                        {
                            players[i].GetComponent<Player1_S>().SetSpeed(0);
                            players[i].GetComponent<Player1_S>().GameEnded();
                        }
                        else
                        {
                            players[i].GetComponent<Player_S>().SetSpeed(0);
                            players[i].GetComponent<Player_S>().GameEnded();
                        }
                    }
                }


                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null)
                    {
                        Debug.Log(enemies[i]);
                        enemies[i].GetComponent<Enemy_S>().GameEnded(won);
                    }
                }

                Debug.Log("LOST");
                uiManager_GM.GameEnded(indexLevel, won);
            }
        }
    }

    public void LoadSelectedScene(int indexSelected)
    {
        musicManager.UpMusic();

        StartCoroutine(StartLoadAsyncScene(indexSelected));
    }

    private IEnumerator StartLoadAsyncScene(int indexLevel)
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexLevel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}
