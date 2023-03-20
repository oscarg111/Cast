using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rbody;
    public Animator anim;
    Vector2 move;

   

    // Update is called once per frame
    void Update()
    {
        // input
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        // adjust animator parameters
        anim.SetFloat("Horizontal", move.x);
        anim.SetFloat("Vertical", move.y);
        anim.SetFloat("speed", move.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        // movement
        rbody.MovePosition(rbody.position + move * speed * Time.fixedDeltaTime);
    }
}
