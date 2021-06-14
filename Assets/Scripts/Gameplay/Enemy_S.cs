using Pathfinding;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_S : MonoBehaviour
{
    [SerializeField]
    private Transform firstTarget;

    [SerializeField]
    private Transform lastTarget_Lost;

    [SerializeField]
    private int difficultyTest = 1;

    [SerializeField]
    private bool gameplayTest = false;

    [SerializeField]
    private float minScale = 0.6f;

    [SerializeField]
    private float maxScale = 0.8f;

    [SerializeField]
    private float scaleLimit = 20f;

    [Header("Difficulty Basic")]
    [SerializeField]
    private float delay_B = 0f;

    [SerializeField]
    private float radiusToSearch_B = 5f;

    [SerializeField]
    private float maxSpeedChasing_B = 3f;

    [SerializeField]
    private float maxSpeedPatrolling_B = 1.5f;


    [Header("Difficulty Expert")]
    [SerializeField]
    private float delay_E = 0f;

    [SerializeField]
    private float radiusToSearch_E = 5f;

    [SerializeField]
    private float maxSpeedChasing_E = 3f;

    [SerializeField]
    private float maxSpeedPatrolling_E = 1.5f;

    // ----
    private IAstarAI agent;

    private GameInstanceScript gameInstance;

    // ----

    private float delay;

    private float radiusToSearch;

    private float maxSpeedChasing;

    private float maxSpeedPatrolling;

    private float switchTime;

    private Transform playerTarget;

    private Vector3 nextPatrolTarget;

    private bool sawPlayer;

    private bool gameStarted = false;

    private bool gameEnded = false;

    private bool enemyLost = false;

    private float inicialDistance;

    private Vector3 inicialScale;

    private AudioSource audioSource;

    [Header("SoundFX")]
    [SerializeField]
    private AudioClip appearClip;

    [SerializeField]
    private AudioClip chaseClip;

    // ---- Layer
    [Header("GFX")]
    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    private GameObject[] players;

    private void Awake()
    {
        agent = GetComponent<IAstarAI>();

        if (!gameplayTest)
        {
            gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();
        }

    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        switchTime = float.PositiveInfinity;
        sawPlayer = false;
        nextPatrolTarget = firstTarget.position;

        int difficulty;

        if (!gameplayTest)
        {
            difficulty = gameInstance.DifficultyLevelIndex;
        }
        else
        {
            difficulty = difficultyTest;
        }


        switch (difficulty)
        {
            // Difficulty Basic
            case 0:
                maxSpeedPatrolling = maxSpeedPatrolling_B;
                maxSpeedChasing = maxSpeedChasing_B;
                agent.maxSpeed = maxSpeedPatrolling;

                radiusToSearch = radiusToSearch_B;
                delay = delay_B;
                break;

            // Difficulty Expert
            case 1:
                maxSpeedPatrolling = maxSpeedPatrolling_E;
                maxSpeedChasing = maxSpeedChasing_E;
                agent.maxSpeed = maxSpeedPatrolling;

                radiusToSearch = radiusToSearch_E;
                delay = delay_E;
                break;

            default:
                maxSpeedPatrolling = maxSpeedPatrolling_B;
                maxSpeedChasing = maxSpeedChasing_B;
                agent.maxSpeed = maxSpeedPatrolling;

                radiusToSearch = radiusToSearch_B;
                delay = delay_B;
                break;
        }

        // LAYER
        players = GameObject.FindGameObjectsWithTag("Player");

        float myY = gameObject.transform.position.y + 1;
        float p1 = players[0].transform.position.y;
        float p2 = players[1].transform.position.y;

        float max = Mathf.Max(myY, p1, p2);
        float min = Mathf.Min(myY, p1, p2);
        if (myY == min)
        {
            mySpriteRenderer.sortingOrder = 4;
        }
        else if (myY == max)
        {
            mySpriteRenderer.sortingOrder = 0;
        }
        else
        {
            mySpriteRenderer.sortingOrder = 2;
        }

    }


    private void Update()
    {
        // LAYER
        if (!gameEnded && gameStarted && !enemyLost)
        {
            float myY = gameObject.transform.position.y + 1;
            float p1 = players[0].transform.position.y;
            float p2 = players[1].transform.position.y;

            float max = Mathf.Max(myY, p1, p2);
            float min = Mathf.Min(myY, p1, p2);
            if (myY == min)
            {

                mySpriteRenderer.sortingOrder = 4;
            }
            else if (myY == max)
            {
                mySpriteRenderer.sortingOrder = 0;
            }
            else
            {
                mySpriteRenderer.sortingOrder = 2;
            }
        }


        if (gameEnded && !gameStarted)
        {
            agent.destination = nextPatrolTarget;
        }


        // CAN START MOVING
        if (!gameStarted) return;


        // AGENT MOVEMENT
        if (agent == null) return;

        if (!sawPlayer) // Patrol
        {
            bool search = false;

            // if the destination can't be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
            if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
            {
                switchTime = Time.time + delay;
            }


            if (Time.time >= switchTime)
            {
                Vector3 newTarget = GetRandomPoint();
                nextPatrolTarget = newTarget;
                search = true;
                switchTime = float.PositiveInfinity;
            }

            agent.destination = nextPatrolTarget;

            if (search) agent.SearchPath();
        }
        else // Chase Player
        {
            agent.destination = playerTarget.position;
        }
    }


    private void FixedUpdate()
    {
        // SCALE
        //Debug.Log(Mathf.Clamp((((1f / 8f) * Mathf.Abs(gameObject.transform.position.y)) + 0.85f), 0.85f, 3f));
        if (!gameEnded)
        {
            float newScale = Mathf.Lerp(minScale, maxScale, Mathf.Abs(transform.position.y / scaleLimit));

            gameObject.transform.localScale = Vector3.one * newScale;
        }
        else if (gameEnded && enemyLost)
        {
            float actualDistance = Vector3.Distance(transform.position, lastTarget_Lost.position);
            float newScale = Mathf.Lerp(0, inicialScale.x, Mathf.Abs(actualDistance / inicialDistance));

            gameObject.transform.localScale = Vector3.one * newScale;
        }

        //gameObject.transform.localScale = Vector3.one * Mathf.Clamp((((1f / 8f) * Mathf.Abs(gameObject.transform.position.y)) + 0.8f), 0.9f, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "PlayerFeet" && !gameEnded)
        {
            // Play Sound
            audioSource.PlayOneShot(chaseClip, 0.6f);
            // ****
            // Set the destination the player Transform
            playerTarget = other.GetComponent<Transform>();
            agent.destination = playerTarget.position;
            if (!agent.pathPending)
            {
                agent.SearchPath();
            }
            sawPlayer = true;
            agent.maxSpeed = maxSpeedChasing;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerFeet" && !gameEnded)
        {
            // Set the destination the last point he saw the player
            nextPatrolTarget = other.GetComponent<Transform>().position;
            agent.destination = nextPatrolTarget;
            if (!agent.pathPending)
            {
                agent.SearchPath();
            }
            sawPlayer = false;
            agent.maxSpeed = maxSpeedPatrolling;
        }
    }


    private Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = Random.insideUnitCircle * radiusToSearch + (Vector2)gameObject.transform.position;
        return randomPoint;
    }

    public void SetSpeed(float speed)
    {
        agent.maxSpeed = speed;
    }

    public void GameStarted()
    {
        gameStarted = true;
        // Play Sound
        audioSource.PlayOneShot(appearClip, 0.6f);
        // ****
    }

    public void GameEnded(bool playersWon)
    {
        gameEnded = true;
        gameStarted = false;
        enemyLost = playersWon;
        if (playersWon)
        {
            agent.maxSpeed = maxSpeedChasing * 2;
            nextPatrolTarget = lastTarget_Lost.position;
            inicialDistance = Vector3.Distance(transform.position, lastTarget_Lost.position);
            inicialScale = transform.localScale;
            // Hide and Disapear
        }
        else
        {
            agent.maxSpeed = 0;
            agent.canMove = false;
            gameObject.transform.localScale *= 1.3f;
            mySpriteRenderer.sortingOrder = 4;
            // Get Big and maybe more bright
        }

    }
}
