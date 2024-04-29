using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ModeScript : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Arena");
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Arena AI");
        }
    }
}
