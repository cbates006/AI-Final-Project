using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This script will detect if the player has fallen from the environment.
 */
public class GameOver : MonoBehaviour
{
    // "readonly" prevents a variable from being changed at runtime.
    public readonly int deathElevation = -150   ;
    public bool gameOver = false;
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y < deathElevation)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
