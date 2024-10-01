using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool canJump;
    private int floorLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.NameToLayer("Floor");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Moving", true);
            transform.position = (Vector2)transform.position + new Vector2(5, 0) * Time.deltaTime;
            sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Moving", true);
            transform.position = (Vector2)transform.position + new Vector2(-5, 0) * Time.deltaTime;
            sr.flipX = true;
        } 
        else if (Input.GetKey(KeyCode.S))
        {
            //crouches
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            animator.SetTrigger("Attack");
            //attacks
        } 
        else if (Input.GetKeyDown(KeyCode.K)) {
            animator.SetTrigger("Block");
            //blocks
        }
    }

    #region Checking floor contact
    void OnCollisionEnter2D(Collision2D coll) {
	    if (isFloor(coll.gameObject)) {
		    canJump = true;
	    }
	}
    
    bool isFloor(GameObject obj) {
		return obj.layer == floorLayer;
	}

    void OnCollisionExit2D(Collision2D coll) {
		if (isFloor(coll.gameObject)) {
			canJump = false;
		}
	}
    #endregion
}
