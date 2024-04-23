using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerHealth health;
    public float speed = 5f;
    public float lookSpeed = 3f;


    // Update is called once per frame
    void Update () {
        // Look
        float yRotation = Input.GetAxis("Mouse X") * lookSpeed;
        float xRotation = Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(Vector3.up, lookSpeed * yRotation * Time.deltaTime);

        // Move
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection *= speed * Time.deltaTime;
        transform.Translate(moveDirection);
	}
}
