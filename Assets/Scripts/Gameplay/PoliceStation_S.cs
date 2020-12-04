using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation_S : MonoBehaviour
{
    private GameplayManager gameplayManager;

    private int allPlayer;

    private void Start()
    {
        allPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            allPlayer--;
            PlayerEntryInPoliceStation();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            allPlayer++;
        }
    }

    private void PlayerEntryInPoliceStation()
    {
        if (allPlayer <= 0)
        {
            gameplayManager.GameEnded(true);
        }        
    }
}
