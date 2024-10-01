using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The bounds beyond which the arrow will be destroyed")]
    private float leftBound = -10.0f; // X position where the arrow should be destroyed

    [SerializeField]
    [Tooltip("Speed at which the arrow moves to the left")]
    private float speed = 5.0f;
    #endregion

    #region Private Variables
    private BoxCollider2D boxCollider;
    #endregion

    #region Initialization
    private void Start()
    {
        // Get the BoxCollider2D component attached to the arrow
        boxCollider = GetComponent<BoxCollider2D>();
    }
    #endregion

    #region Update Method
    private void Update()
    {
        // Move the arrow to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Destroy the arrow if it moves beyond the leftmost edge of the screen
        if (transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
