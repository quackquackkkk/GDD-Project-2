using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    public string swapTo;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            SceneManager.LoadScene(swapTo);
        }
    }

    public void toLevel1() {
        SceneManager.LoadScene("Level_1");
    }
    public void toLevel2()
    {
        SceneManager.LoadScene("Level_2 (Arrows)");
    }

    public void toMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
