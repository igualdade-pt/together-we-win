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

    private GameObject[] enemies;

    private GameObject otherPlayer;

    private GameObject enemy;

    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();


        // LAYER
        players = GameObject.FindGameObjectsWithTag("Player");

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != gameObject)
            {
                otherPlayer = players[i];
                Debug.Log(players[i]);
            }
        }

        enemy = enemies[0];

        float myY = gameObject.transform.position.y;
        float otherY = otherPlayer.transform.position.y;
        float enem = enemy.transform.position.y + 1;

        float max = Mathf.Max(myY, otherY, enem);
        float min = Mathf.Min(myY, otherY, enem);

        if (myY == min)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 2;
        }
        else if (myY == max)
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 0;
        }
        else
        {
            gameObject.GetComponent<Renderer>().sortingOrder = 1;
        }
    }


    private void Update()
    {
        // LAYER
        float myY = gameObject.transform.position.y;
        float otherY = otherPlayer.transform.position.y;
        float enem = enemy.transform.position.y + 1;

        float max = Mathf.Max(myY, otherY, enem);
        float min = Mathf.Min(myY, otherY, enem);
        if (myY == min)
        {

            gameObject.GetComponent<Renderer>().sortingOrder = 2;
        }
        else if (myY == max)
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

        // SCALE
        //Debug.Log(Mathf.Clamp((((1f / 8f) * Mathf.Abs(gameObject.transform.position.y)) + 0.85f), 0.85f, 3f));

        gameObject.transform.localScale = Vector3.one * Mathf.Clamp((((1f / 7f) * Mathf.Abs(gameObject.transform.position.y)) + 0.5f), 0.73f, 3f);
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
            gameplayManager.GameEnded(true);
        }
        if (other.tag == "EnemyCollider")
        {
            gameplayManager.GameEnded(false);
        }
    }

    public void SetSpeed(float temp)
    {
        speed = temp;
    }
}
