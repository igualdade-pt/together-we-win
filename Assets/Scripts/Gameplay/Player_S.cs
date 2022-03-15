﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player_S : MonoBehaviour
{
    //[SerializeField]
    //private bool mobile = true;

    [SerializeField]
    private int playerNumber;

    [SerializeField]
    private Joystick joyStick;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private Sprite oceanSprite;

    [SerializeField]
    private Sprite mySprite;

    private Rigidbody2D myRigid;

    private Animator myAnimator;

    private bool facingRight = true;

    private bool gameStarted = false;

    private bool gameEnded = false;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask layerWallMask;

    [SerializeField]
    private bool havePoliceStation = false;

    [Header("Properties")]
    [Space]
    [SerializeField]
    private float speedLimit = 1f;

    [SerializeField]
    private float minSpeedX = 4f;

    [SerializeField]
    private float maxSpeedX = 8f;

    [SerializeField]
    private float minSpeedY = 2f;

    [SerializeField]
    private float maxSpeedY = 4f;

    [SerializeField]
    private float minScale = 1f;

    [SerializeField]
    private float maxScale = 3f;

    [SerializeField]
    private float scaleLimit = 20f;

    [SerializeField]
    private float speed = 1f;

    private GameplayManager gameplayManager;

    // ---- Layer
    private GameObject[] players;

    private GameObject[] enemies;

    private GameObject otherPlayer;

    private GameObject enemy;

    private int myIndex;

    private void Awake()
    {
        facingRight = !mySpriteRenderer.flipX;
    }

    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
    }

    private void Update()
    {
        if (!gameEnded)
        {
#if !UNITY_WEBGL
            {
                // MOBILE
                if (Input.touchCount > 0)
                {
                    Touch[] myTouches = Input.touches;
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(myTouches[i].position);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray, layerMask);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider == gameObject.GetComponent<Collider2D>())
                        {
                            myIndex = i;
                            Vector2 mousePos = Camera.main.ScreenToWorldPoint(myTouches[i].position);

                            Vector3 compareFlip = (ray - GetComponent<Transform>().position).normalized;

                            RaycastHit2D hitWall = Physics2D.Linecast(ray, ray, layerWallMask);
                            Debug.Log(hitWall.collider);
                            if (hitWall.collider == null)
                            {
                                GetComponent<Transform>().position = mousePos;
                            }


                            if (myTouches[i].deltaPosition == Vector2.zero)
                            {
                                myAnimator.SetFloat("speed", Mathf.Abs(0));
                            }
                            else
                            {
                                myAnimator.SetFloat("speed", Mathf.Abs(1));
                            }


                            if (compareFlip.x > 0 && !facingRight)
                            {
                                Flip();
                            }
                            if (compareFlip.x < 0 && facingRight)
                            {
                                Flip();
                            }
                        }
                        else if (i == myIndex)
                        {
                            myAnimator.SetFloat("speed", Mathf.Abs(0));
                        }
                    }

                    if (!gameStarted)
                    {
                        gameStarted = true;
                        gameplayManager.GameStarted(this);
                    }
                }
                else
                {
                    myAnimator.SetFloat("speed", Mathf.Abs(0));
                }
            }    
#else
                // PC
                if (myRigid.velocity != Vector2.zero)
                    {
                        if (!gameStarted)
                        {
                            gameStarted = true;
                            gameplayManager.GameStarted(this);
                        }
                    }
#endif
        }
    }

    private void FixedUpdate()
    {
        if (!gameEnded)
        {
#if UNITY_WEBGL
                //PC
                switch (playerNumber)
                {
                    case 1:
                        float horizontalMove1 = Input.GetAxisRaw("Horizontal1");
                        float VerticalMove1 = Input.GetAxisRaw("Vertical1");

                        if (horizontalMove1 > 0 & !facingRight)
                        {
                            Flip();
                        }

                        if (horizontalMove1 < 0 & facingRight)
                        {
                            Flip();
                        }

                        myRigid.velocity = new Vector2(horizontalMove1 * speed, VerticalMove1 * speed);
                        if (VerticalMove1 != 0 || horizontalMove1 != 0)
                        {
                            myAnimator.SetFloat("speed", 1);
                        }
                        else
                        {
                            myAnimator.SetFloat("speed", 0);
                        }

                        break;

                    case 0:
                        float horizontalMove2 = Input.GetAxisRaw("Horizontal2");
                        float VerticalMove2 = Input.GetAxisRaw("Vertical2");

                        if (horizontalMove2 > 0 & !facingRight)
                        {
                            Flip();
                        }

                        if (horizontalMove2 < 0 & facingRight)
                        {
                            Flip();
                        }

                        myRigid.velocity = new Vector2(horizontalMove2 * speed, VerticalMove2 * speed);
                        if (VerticalMove2 != 0 || horizontalMove2 != 0)
                        {
                            myAnimator.SetFloat("speed", 1);
                        }
                        else
                        {
                            myAnimator.SetFloat("speed", 0);
                        }

                        break;

                    default:
                        break;
                
            }
#endif
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
        if (other.tag == "Player" && !havePoliceStation)
        {
            gameplayManager.GameEnded(true);
        }
        if (other.tag == "EnemyCollider")
        {
            gameplayManager.GameEnded(false);
        }
        if (other.tag == "Ocean")
        {
            // mySpriteRenderer.sprite = oceanSprite;
            myAnimator.SetBool("ocean", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ocean")
        {
            //mySpriteRenderer.sprite = mySprite;
            myAnimator.SetBool("ocean", false);
        }
    }

    public void SetSpeed(float temp)
    {
        speed = temp;
#if UNITY_WEBGL
        myRigid.velocity = Vector2.zero;        
#endif
        gameEnded = true;
        myAnimator.SetFloat("speed", Mathf.Abs(0));
    }

    public void GameEnded()
    {
        myAnimator.SetTrigger("lost");
        gameEnded = true;
        myAnimator.SetFloat("speed", Mathf.Abs(0));
    }

    public void GameStarted()
    {
        gameStarted = true;
    }
}
