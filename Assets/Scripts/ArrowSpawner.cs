using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The bounds of the spawner")]
    private Vector3 m_Bounds;

    [SerializeField]
    [Tooltip("The arrow prefab to spawn")]
    private GameObject arrowPrefab; // Reference to the arrow prefab

    [SerializeField]
    [Tooltip("Sprite to assign to the arrow")]
    private Sprite arrowSprite; // Reference to the sprite to assign
    #endregion

    private int num_Arrows = 100;
    private float spawn_Time = 3f;
    private bool cleared = false;

    #region Initialization
    private void Awake()
    {
        StartSpawning();
    }
    #endregion

    #region Spawn Methods
    public void StartSpawning()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int i = 0;
        yield return new WaitForSeconds(10);
        // Spawn while level is not cleared
        while (!cleared && i < num_Arrows)
        {
            yield return new WaitForSeconds(spawn_Time);

            // Random spawn position
            Vector3[] spawnPos = {
                new Vector3(15.0f, -2.98f, 0.0f),
                new Vector3(15.0f, -1.2f, 0.0f),
                new Vector3(15.0f, 0.55f, 0.0f)
            };

            int randomNumber = Random.Range(0, spawnPos.Length);

            // Instantiate the arrow at a random position from the spawnPos array
            GameObject arrowInstance = Instantiate(arrowPrefab, spawnPos[randomNumber], Quaternion.identity);
            i++;
        }
    }

    private bool Level_Cleared()
    {
        return cleared;
    }
    #endregion
}
