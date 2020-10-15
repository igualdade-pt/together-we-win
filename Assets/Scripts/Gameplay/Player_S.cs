using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_S : MonoBehaviour
{
    [SerializeField]
    private Joystick joyStick;

    private Rigidbody2D myRigid;

    // Start is called before the first frame update
    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 move = joyStick.Direction * 4f;
        myRigid.velocity = move;
    }
}
