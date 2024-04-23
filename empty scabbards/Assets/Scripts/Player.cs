using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class comments for documentation go here. See the C# documentation for
 * formatting tips.
 */
public class Player : MonoBehaviour
{
    public float horizontalScale = 1f;
    public float jumpStrength = 5;
    private Rigidbody rigid; //Rigidbody2D of player object
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        // TODO: what if there isn't one?
    }

    // Update is called once per frame
    /// <summary>
    ///  Uses a simple (and not very good) way to detect if player is jumping
    /// </summary>
    private void Update()
    {
        if (rigid.velocity.y > 0)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }

    /// <summary>
    /// FixedUpdate is recommended for physics operations requiring small variations
    /// in time between calls.
    /// </summary>
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * horizontalScale;
        float jumpInput = 0f;
        if (!jumping)
        {
            jumpInput = Input.GetAxis("Jump") * jumpStrength;
        }
        rigid.AddForce(new Vector2(horizontalInput, 0));
        // TODO: what if we use ForceMode2D.Impulse?
        rigid.AddForce(new Vector2(0, jumpInput), ForceMode.Impulse);
        jumping = true;
    }
}
