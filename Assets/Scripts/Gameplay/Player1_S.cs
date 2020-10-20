using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // MOVEMENT
        Vector2 move = joyStick.Direction * 4f;
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
    }

    private void Flip()
    {
        Vector3 myLocalRotation = transform.localEulerAngles;
        myLocalRotation.y += 180f;
        transform.localEulerAngles = myLocalRotation;
        facingRight = !facingRight;
    }
}
