using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootCollider : MonoBehaviour
{
    private PlayerControl playerControl;

    void Start()
    {
        // Find the PlayerControl script from the parent GameObject
        playerControl = GetComponentInParent<PlayerControl>();
    }

    #region Floor_contact_methods
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (playerControl.isFloor(coll.gameObject))
        {
            playerControl.onFloor = true;
            playerControl.jumps = 2;
        }
<<<<<<< HEAD
        print("plz");
=======
>>>>>>> 62a30b5afb1e2976ef12b7a8abb8ec3131ef41df
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (playerControl.isFloor(coll.gameObject))
        {
            playerControl.onFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (playerControl.isFloor(coll.gameObject))
        {
            playerControl.onFloor = false;
        }
    }
    #endregion
}
