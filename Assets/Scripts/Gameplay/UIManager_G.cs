using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_G : MonoBehaviour
{

    private GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
    }


    public void UpdateLanguage(int indexLanguage)
    {

    }
}
