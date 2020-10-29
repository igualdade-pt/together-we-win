using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player1_S : MonoBehaviour
{
    [SerializeField]
    private Joystick joyStick;

    private Rigidbody2D myRigid;

    private Animator myAnimator;

    private bool facingRight = true;

    [Header("Properties")]
    [SerializeField]
    private float speed = 4f;

    private GameplayManager gameplayManager;

    private GameObject[] players;

    private GameObject otherPlayer;

    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
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

        if (move.x > 0 && !facingRight)
        {
            Flip();
        }
        if (move.x < 0 && facingRight)
        {
            Flip();
        }
        myRigid.velocity = move;
        if (move.x >= 0.1 || move.y != 0)
        {
            myAnimator.SetFloat("speed", Mathf.Abs(1));
        }
        else
        {
            myAnimator.SetFloat("speed", Mathf.Abs(0));
        }


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

    private void Flip()
    {
        Vector3 myLocalRotation = transform.localEulerAngles;
        myLocalRotation.y += 180f;
        transform.localEulerAngles = myLocalRotation;
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<Player_S>().SetSpeed(0);
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
