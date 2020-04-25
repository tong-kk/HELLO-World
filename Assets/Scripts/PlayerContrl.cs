﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    public float moveForce = 400f;
    public float maxSpeed = 5f;
    public float jumpForce = 100;
    [HideInInspector]
    public bool jump = false;

    private bool grounded = false;
    private Transform groundCheck;
    private Rigidbody2D heroBody;
    [HideInInspector]
    public bool faceRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void FixedUpdate()
    {
        //获取水平方向输入
        float h = Input.GetAxis("Horizontal");

        //判断是否超过最大速度
        if (h * heroBody.velocity.x < maxSpeed)
        {
            //heroBody.AddForce(Vector2.right * h * moveForce);
            heroBody.velocity += Vector2.right * h * moveForce;
        }
            
        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
        {
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,
                                            heroBody.velocity.y);
        }
            

        if(h>0 && !faceRight)
            flip();
        if (h < 0 && faceRight)
            flip();

        if (jump)
        {
            heroBody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position,
                                        1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    void flip()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
