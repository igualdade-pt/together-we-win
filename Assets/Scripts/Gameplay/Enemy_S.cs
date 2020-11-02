﻿using Pathfinding;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_S : MonoBehaviour
{
    [SerializeField]
    private Transform firstTarget;

    [SerializeField]
    private int difficultyTest = 1;

    [SerializeField]
    private bool gameplayTest = false;

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
    }


    private void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerFeet")
        {
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
        if (other.tag == "PlayerFeet")
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

    public void SetSpeed(float temp)
    {
        agent.maxSpeed = temp;
    }

    public void GameEnded(bool won)
    {
        if (won)
        {
            // Hide and Disapear
        }
        else
        {
            // Get Big and maybe more bright
        }

    }
}