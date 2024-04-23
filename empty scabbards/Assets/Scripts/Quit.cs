using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class quits the application as soon as a key is pressed.
 * TODO: this only works with a build. How do I make it work in the editor?
 */
public class Quit : MonoBehaviour
{
 

    void Update()
    {
        // Quit when any key is pressed
        if (Input.anyKey)
        {
            //If we are running in a standalone build of the game
#if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

