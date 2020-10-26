using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player_S : MonoBehaviour
{
    [SerializeField]
    private Joystick joyStick;

    private Rigidbody2D myRigid;

    [Header("Properties")]
    [SerializeField]
    private float speed = 4f;

    private GameplayManager gameplayManager;

    private GameObject [] players;

    private GameObject otherPlayer;

    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();


        // LAYER
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != gameObject)
            {
                otherPlayer = players[i];
                Debug.Log(players[i]);
            }
        }

        float myY = gameObject.transform.position.y;
        float otherY = otherPlayer.transform.position.y;
        if (myY > otherY)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 0;
        }
        else
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 1;
        }

    }


    private void FixedUpdate()
    {
        // MOVEMENT
        Vector2 move = joyStick.Direction * speed;
        myRigid.velocity = move;


        // LAYER
        float myY = gameObject.transform.position.y;
        float otherY = otherPlayer.transform.position.y;
        if (myY > otherY)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 0;
        }
        else
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<Player1_S>().SetSpeed(0);
            gameplayManager.GameEnded(true);
        }
        if (other.tag == "Enemy")
        {
            gameplayManager.GameEnded(false);
        }
    }

    public void SetSpeed(float temp)
    {
        speed = temp;      
    }
}
