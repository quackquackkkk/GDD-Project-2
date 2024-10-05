using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float crouchHeight = 1.05f;
    private float standHeight = 2.3f;
    private float moveSpeed;
    public int jumps;
    private bool standing = true;
    private bool crouched;
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.NameToLayer("Floor");
        col = GetComponent<BoxCollider2D>();
        moveSpeed = 5.0f;
    }

    void Update()
    {
        // Jump Control
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0 && crouched == false) {
            animator.SetTrigger("Jump");
            jumps -= 1;
            Jump();
        }
        // Crouch Control
        if (onFloor && (Input.GetKey(KeyCode.S) || !canStand())) {
            moveSpeed = 3.0f;
            animator.SetBool("Crouched", true);
            crouched = true;
            Crouch();
        }
        else {
            moveSpeed = 5.0f;
            animator.SetBool("Crouched", false);
            crouched = false;
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

    #region Death
    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Enemy")) {
            SceneManager.LoadScene("GameOver");
        }
    }
    #endregion

    #region Crouch_stand_jump_methods
    private void Crouch() {
        col.size = new Vector2(col.size.x, crouchHeight);
        col.offset = new Vector2(0.1f, -0.6f);
        standing = false;
    }

    private void Stand() {
        col.size = new Vector2(col.size.x, standHeight);
        col.offset = new Vector2(0.1f, 0.02f);
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
        LayerMask ceiling = LayerMask.GetMask("Ceiling");
        LayerMask wall = LayerMask.GetMask("Wall");
        LayerMask mask = ceiling | wall;
        if (Physics2D.Raycast(rb.position, rb.transform.up, crouchHeight, mask)) {
            return false;
        }
        return true;
    }
    #endregion
}
