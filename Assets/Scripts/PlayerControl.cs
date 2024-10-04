using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    #region Physics_and_sprite_variables
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    #endregion

    #region Animation_variables
    private Animator animator;
    #endregion

    #region Floor_variables
    public bool onFloor;
    public int floorLayer;
    #endregion

    #region Collider_crouch_jump_variables
    private BoxCollider2D col;
    private float crouchHeight = 1.25f;
    private float standHeight = 2.5f;
    private float moveSpeed;
    public int jumps;
    private bool standing = true;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.NameToLayer("Floor");
        col = GetComponent<BoxCollider2D>();
        moveSpeed = 5;
    }

    void Update()
    {
        // Jump Control
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0) {
            animator.SetTrigger("Jump");
            jumps -= 1;
            Jump();
        }
        // Crouch Control
        if (onFloor && (Input.GetKey(KeyCode.S) || !canStand())) {
            moveSpeed = 4;
            animator.SetBool("Crouched", true);
            Crouch();
        }
        else {
            moveSpeed = 5;
            animator.SetBool("Crouched", false);
            Stand();
        }
        // Left Right Movement Control
        if (Input.GetKey(KeyCode.D)) {
            animator.SetBool("Moving", true);
            transform.position = (Vector2)transform.position + new Vector2(moveSpeed, 0) * Time.deltaTime;
            sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A)) {
            animator.SetBool("Moving", true);
            transform.position = (Vector2)transform.position + new Vector2(-moveSpeed, 0) * Time.deltaTime;
            sr.flipX = true;
        } 
        else {
            animator.SetBool("Moving", false);
        }
        // Attack/Defend Control
        if (Input.GetKeyDown(KeyCode.J)) {
            animator.SetTrigger("Attack");
            //attacks
        } 
        else if (Input.GetKeyDown(KeyCode.K)) {
            animator.SetTrigger("Block");
            //blocks
        }
    }

/*#region Floor_contact_methods
    void OnCollisionEnter2D(Collision2D coll) {
	    if (isFloor(coll.gameObject)) {
		    onFloor = true;
            jumps = 2;
	    }
	}

    void OnCollisionStay2D(Collision2D coll) {
        if (isFloor(coll.gameObject)) {
		    onFloor = true;
	    }
    }
    
    bool isFloor(GameObject obj) {
		return obj.layer == floorLayer;
	}

    void OnCollisionExit2D(Collision2D coll) {
		if (isFloor(coll.gameObject)) {
			onFloor = false;
		}
	}
    #endregion*/

    #region Crouch_stand_jump_methods
    private void Crouch() {
        col.size = new Vector2(col.size.y, crouchHeight);
        col.offset = new Vector2((float) 0.1, (float) -0.75);
        standing = false;
    }

    private void Stand() {
        col.size = new Vector2(col.size.y, standHeight);
        col.offset = new Vector2((float) 0.1, (float) -0.1);
        standing = true;
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
    }
    public bool isFloor(GameObject obj)
    {
        return obj.layer == floorLayer;
    }
    private bool canStand() {
        if (standing) {
            return true;
        }
        LayerMask mask = LayerMask.GetMask("Ceiling");
        if (Physics2D.Raycast(rb.position, rb.transform.up, crouchHeight, mask)) {
            return false;
        }
        return true;
    }
    #endregion
}
